using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloBilling.Common; 
using gloSettings;
using C1.Win.C1FlexGrid;
using gloDatabaseLayer;
using gloAuditTrail;
using gloBilling.EOBPayment;
using gloOffice;
using gloBilling.Statement;
using System.Runtime.InteropServices;
using gloGlobal;
using System.Linq;
using System.Reflection;
using gloBilling;
using C1.C1Excel;
using System.Collections;
using gloPatientStripControl;
using gloBilling.C1GridFilter;
using gloBilling.Properties;
using gloBilling.Collections;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Resources;

namespace gloAccountsV2
{
    public partial class frmPatientFinancialViewV2 : Form
    {

        #region "Variable declaration"      
        TabPage objtab = null;
        Int64 _nPatientID = 0;
        Int64 _nTrasactionID;
        Int64 nGlobalPatientID = 0;

        const int COL_NOTE_IMAGE = 5;
        const int COL_CLAIM_NO = 7;
        const int COL_TRANSACTION_MST_ID = 34;
        private int iSummarySelRow = 1;
        private int iChargesSelRow = 1;
        private int iPaymentSelRow = 1;
        private int iReserveSelRow = 1;


        private Font _fBold = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
        private Font _fRegular = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
   //     private Boolean _blnShowZeroBal = true;

        private string _strTagClaims = "Charges & Claims";
        string strReportProtocol = string.Empty;
        private string _strTagPaymentReserves = "Patient Payments & Reserves";
     //   private bool _IsFormLoading = false;
        private bool _IsCalledFromInsPmt = false;
        private bool _blnDisposed;
        private static frmPatientFinancialViewV2 frmPatFinView;
        private BackgroundWorker worker = new BackgroundWorker();
        private BackgroundWorker Summaryworker = new BackgroundWorker();
        private Boolean _isFollowUpFeatureEnabled = false;
        private Boolean isBadDebtPatient = false;
        bool IsProviderEnable = false;
        Char _AgingSortByInSummary = 'R';

        Int64 _nPAccountId = 0;
        Int64 _nSelectedPatientId = 0;
        Int64 _nGuarantorId = 0;
        Int64 _nAccountPatientId = 0;

        String _reportName = String.Empty;
        String _parameterName = String.Empty;
        String _ParameterValue = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings =System.Configuration.ConfigurationManager.AppSettings;

        string[] strArrClaims = default(string[]);
        DataTable _dtPALog = null;

        public bool ShowGlobalperiod { get; set; }
        public bool IsCalledFromInsPmt
        {
            get { return _IsCalledFromInsPmt; }
            set { _IsCalledFromInsPmt = value; }
        }
        public Int64 _nSelectAccountId { get; set; }

 
        public Tuple<Int64, string, string> SyncPatientId
        {
            get { return new Tuple<Int64, string, string>(_nPatientID, oPatientControl.PatientCode, oPatientControl.PatientName); }
        }

        AccountLogTypeFilter filter = null;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        // We need to use unmanaged code
        [DllImport("user32.dll")]
        static extern bool LockWindowUpdate(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        // Nested Types
        private enum _tabType
        {
            Summary = 0,
            Claim_Details = 1,
            Patient_Payment_and_Reserves = 2,
            Report = 3,
            Statement = 4,
            Insurance_Reserves_and_Refunds = 5,
            Chronology = 6,
            Insurance_Remittance = 7,
            //Party = 27
        }

        private enum GridColumnAccountLog
        {
            NoteId,
            EOBID,
            DebitId,
            CreditId,           
            MstTransactionID,
            TransactionDetailID,
            TransactionID,          
            RefundID,
            CloseDate,
            PatientID,
            Patient,
            Type,
            LogDesc,
            Delta,
            Balance,
            PatDue,
            User,
            Datetime            
        }

        public enum OnlineActivity
        {
            PaymentPlan=0,
            OneTimePayment=1,
            Subscriptions=2,
            Transaction=3,
            Encounter=4,
            All=5
        }
       private ClearGage.SSO.SsoHelper ssoHelper;

        #region  " Grid Constants "

        const int COL_EOBPAYMENTID = 0;
        const int COL_PAYMENTCLOSEDATE = 1;
        const int COL_COMPANYNAME = 2;
        const int COL_ORIGINALPAYMENT = 3;//Check Number,Date,Amount
        const int COL_ASSO_CLAIMNO = 4;
        const int COL_USERNAME = 5;
        const int COL_TORESERVES = 6;//Amount for reserve
        const int COL_TYPE = 7;//Copay,Advance,Other
        const int COL_NOTE = 8;//Note
        const int COL_AVAILABLE = 9;//Available amount
        const int COL_USERESERVE = 10;//Used Reserve
        const int COL_REFUND = 11;//Current amount to use from avaiable amount
        const int COL_PAYMODE = 12;
        const int COL_SOURCE = 13; //Patient or Insurance Name
        const int COL_PAYMENTMODE = 14;
        const int COL_PAYMENTMODENO = 15;
        const int COL_ASSO_PATIENTID = 16;
        const int COL_ASSO_PATIENTNAME = 17;
        const int COL_ASSO_TRACKTRANSACTIONID = 18;
        const int COL_ASSO_MSTTRANSACTIONID = 19;
        const int COL_COUNT = 20;

        const int COL_CLOSEDATE_REF = 0;
        const int COL_TRAY_REF = 1;
        const int COL_COMPANY_REF = 2;
        const int COL_CHECK_NO_REF = 3;
        const int COL_REFUND_DATE_REF = 4;
        const int COL_REFUND_AMOUNT_REF = 5;
        const int COL_CLAIMNO_REF = 6;
        const int COL_USER_REF = 7;
        const int COL_NOTE_REF = 8;
        const int COL_STATUS_REF = 9;
        const int COL_EOBPAYMENT_ID_REF = 10;
        const int COL_COMPANY_ID_REF = 11;
        const int COL_PAYMENT_TRAY_ID_REF = 12;
        const int COL_USER_ID_REF = 13;
        const int COL_DEBIT_AMOUNT_REF = 14;
        const int COL_DATETIME_REF = 15;
        const int COL_REFUNDID_REF = 16;
        const int COL_PATIENTNAME_REF = 17;
        const int COL_COUNT_REF = 18;

        #region  " Grid Constants "



        #endregion

        #endregion
       
        #endregion

       

        #region "Constructor"


        public frmPatientFinancialViewV2(Int64 PatientID)
        {
            InitializeComponent();
            _nPatientID = PatientID;
            nGlobalPatientID = PatientID;
            ClearGage.clsCleargage oclsCleargage = new ClearGage.clsCleargage();
            ssoHelper = oclsCleargage.InitiateSOSHelper(gloPMGlobal.DatabaseConnectionString);
            SetClearGageCallbacks(ssoHelper);
        }

        

        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this._blnDisposed))
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
                    try
                    {
                        if (contextMenuChargeHistory != null)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(contextMenuChargeHistory);
                            if (contextMenuChargeHistory.Items != null)
                            {
                                contextMenuChargeHistory.Items.Clear();

                            }
                            contextMenuChargeHistory.Dispose();
                            contextMenuChargeHistory = null;
                        }
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
            }
            frmPatFinView = null;
            this._blnDisposed = true;
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

        ~frmPatientFinancialViewV2()
        {
            Dispose(false);
        }

        public static frmPatientFinancialViewV2 GetInstance(Int64 PatientID)
        {
            try
            {
                if (frmPatFinView == null)
                {
                    frmPatFinView = new frmPatientFinancialViewV2(PatientID);
                }
            }
            finally
            {

            }
            return frmPatFinView;
        }

        #endregion

        #region " Events"

        #region 'Form Event'

        private void frmPatientFinancialView_Load(object sender, EventArgs e)
        {
            GeneralSettings oSettings = null;
            gloOffice.gloC1FlexStyle.Style(c1FlexGridChargesClaims, false);
            gloOffice.gloC1FlexStyle.Style(c1FlexGrid_Statements, false);
            tbpgReport.Tag = "";
            try
            {
                IsProviderEnable = gloAccountsV2.gloBillingCommonV2.IsPatientReserve_ProviderEnable();
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);


                Summaryworker.WorkerSupportsCancellation = true;
                Summaryworker.DoWork += new DoWorkEventHandler(Summaryworker_DoWork);
                Summaryworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Summaryworker_RunWorkerCompleted);

                LockWindowUpdate(this.Handle);
                this.Focus();
                objtab = tbPatientFinancial.TabPages["tbpgInsResAndRefund"];
                LoadPatientStrip(_nPatientID, 0, true);
                
                gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                isBadDebtPatient = oSecurity.isBadDebtPatient(_nPatientID, true);
                oSecurity.Dispose();
                oSecurity = null;
                
                //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
                gloPatientFinancialViewV2 objPatFinacialView = null;
                if (oPatientControl.IsAllAccPatSelected)
                {
                    objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
                }
                else
                {
                    objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
                }

                tbPatientFinancial_SelectedIndexChanged(null, null);
                TabPage newPage = tbPatientFinancial.TabPages["tbpgInsResAndRefund"];
                if (!objPatFinacialView.chkIsInsReserveRefundExist())
                {
                    tbPatientFinancial.TabPages.RemoveAt(tbPatientFinancial.TabPages.IndexOf(newPage));
                }                               

                tbPatientFinancial.TabPages.RemoveAt(7);
                tbPatientFinancial.TabPages.RemoveAt(5);
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }

                #region " Account Log Feature Enable Disable according to settings."

                oSettings = new GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                string sType = oSettings.GetInstallationType(0, 1);
                object oValue = null;
                bool SettingsValue = false;
                oSettings.GetSetting("FOLLOWUP_FEATURE", 0, gloPMGlobal.ClinicID, out oValue);
                Boolean.TryParse(Convert.ToString(oValue), out SettingsValue);
                if (!SettingsValue)
                {
                    TabPage AccountLogPage = tbPatientFinancial.TabPages["tbpgAccountLog"];
                    tbPatientFinancial.TabPages.RemoveAt(tbPatientFinancial.TabPages.IndexOf(AccountLogPage));
                    _isFollowUpFeatureEnabled = SettingsValue;
                } 

                #endregion

                if (ShowGlobalperiod)
                {
                    tbPatientFinancial.SelectedTab = tbPatientFinancial.TabPages["tbpgGlobalPeriods"];
                }

                ApplyFilterToAccountLogGrid();
                //if (isBadDebtPatient)
                //{
                //    tsb_btnCollAgencyRefund.Visible = true;
                //}
                //else
                //{
                //    tsb_btnCollAgencyRefund.Visible = false;
                //}
                if (!gloGlobal.gloPMGlobal.IsCleargageEnable)
                {
                    tbPatientFinancial.TabPages.Remove(tbpgPatOnlineActivity);
                }

                dtStartDate.Text = (DateTime.Now).AddMonths(-3).ToString();
                dtEndDate.Text = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                LockWindowUpdate(IntPtr.Zero);
                if (oSettings != null) { oSettings.Dispose(); }
            }

        }
       
        private void frmPatientFinancialView_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //added by mahesh s on 13-may-2011 for null handling.
                if (oPatientControl != null)
                    oPatientControl.Dispose();

                oPatientControl = null;

                c1FlexGridSummary.DataSource = null;
                c1PALogView.DataSource = null;
                c1FlexGridChargesClaims.DataSource = null;
                c1FlexGridPmnt.DataSource = null;
                c1FlexGridAvailResrv.DataSource = null;
                c1PatientRefund.DataSource = null;
                c1FlexGrid3.DataSource = null;
                c1FlexGrid_Statements.DataSource = null;
                c1FlexGridChronology.DataSource = null;
                c1Reserve.DataSource = null;
                c1InsuranceRefundLog.DataSource = null;
                c1GlobalPeriods.DataSource = null;

                c1FlexGridSummary.Styles.Clear();
                c1PALogView.Styles.Clear();
                c1FlexGridChargesClaims.Styles.Clear();
                c1FlexGridPmnt.Styles.Clear();
                c1FlexGridAvailResrv.Styles.Clear();
                c1PatientRefund.Styles.Clear();
                c1FlexGrid3.Styles.Clear();
                c1FlexGrid_Statements.Styles.Clear();
                c1FlexGridChronology.Styles.Clear();
                c1Reserve.Styles.Clear();
                c1InsuranceRefundLog.Styles.Clear();
                c1GlobalPeriods.Styles.Clear();

                //_fBold.Dispose();
                //_fBold = null;
                //_fRegular.Dispose();
                //_fRegular = null;

                //gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                //oSettings.WriteSettings_XML("PatientFinancialView", "IsModified", Boolean.TrueString);
                //oSettings.Dispose();
                //oSettings = null;

                gloGlobal.gloPMGlobal.PatientFinancialViewIsModified = true;

                this.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        void frmPatStmt_on_FromClose(object sender, EventArgs e)
        {
            if (!this._blnDisposed)
            {
                tbPatientFinancial_SelectedIndexChanged(null, null);
            }
            this.Focus();
        }

        private void frmPatientFinancialViewV2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worker.IsBusy)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }

            if (Summaryworker.IsBusy)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }               

        #endregion

        #region 'ToolStrip Button Event'

        private void ts_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_ShowHideZeroBalance_Click(object sender, EventArgs e)
        {
            // _blnShowZeroBal = false;
            tsb_HideZeroBalance.Visible = true;
            tsb_ShowZeroBalance.Visible = false;
            chkIncludeVoids.Visible = true;
            chkIncludeVoids.Checked = false;
            showHideZeroBalances();

        }

        private void tsb_HideZeroBalance_Click(object sender, EventArgs e)
        {
            // _blnShowZeroBal = true;
            tsb_ShowZeroBalance.Visible = true;
            tsb_HideZeroBalance.Visible = false;
            chkIncludeVoids.Visible = false;
            showHideZeroBalances();
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            }

            try
            {
                oPatientControl.RefreshData();
                RemoveTabPageTag();
                if (!objPatFinacialView.chkIsInsReserveRefundExist())
                {
                    TabPage newPage = tbPatientFinancial.TabPages["tbpgInsResAndRefund"];
                    if (newPage != null)
                    {
                        if (tbPatientFinancial.TabPages.Contains(newPage))
                        {
                            if (tbPatientFinancial.SelectedTab == newPage)
                            {
                                tbPatientFinancial.SelectedIndex = 0;
                            }
                            tbPatientFinancial.TabPages.Remove(newPage);
                        }
                    }
                }
                else
                {
                    TabPage newPage = tbPatientFinancial.TabPages["tbpgInsResAndRefund"];
                    if (newPage == null)
                    {
                        tbPatientFinancial.TabPages.Add(objtab);
                    }
                }
                if (tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Claim_Details))
                {
                    rbtn_Claim.Checked = true;
                    tsb_ShowZeroBalance.Visible = true;
                    tsb_HideZeroBalance.Visible = false;
                    chkIncludeVoids.Visible = false;
                    c1FlexGridChargesClaims.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, COL_CLAIM_NO);
                }
                tbPatientFinancial_SelectedIndexChanged(null, null);
                if (tbPatientFinancial.SelectedTab.Name == "tbpgGlobalPeriods")
                {
                    FillGlobalPeriods();
                }

                if (tbPatientFinancial.SelectedTab.Name == "tbpgAccountLog")
                {
                    pnlProgressIndication.BringToFront();
                    //worker.WorkerSupportsCancellation = true;
                    //worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    //worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    if (!worker.IsBusy)
                    {
                        worker.RunWorkerAsync();
                    }
                }

                if (tbPatientFinancial.SelectedTab.Name == "isSummaryLoaded")
                {
                    pnlProgressIndication.BringToFront();
                    //worker.WorkerSupportsCancellation = true;
                    //worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    //worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    if (!Summaryworker.IsBusy)
                    {
                        Summaryworker.RunWorkerAsync();
                    }
                }
                //if (tbPatientFinancial.SelectedTab.Name=="tbpgPatOnlineActivity")
                //{
                //    FillOnlineActvity();
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                if (objPatFinacialView != null)
                {
                    objPatFinacialView.Dispose();
                }
            }
            finally
            {
                if (objPatFinacialView != null)
                {
                    objPatFinacialView.Dispose();
                }
            }

        }

        // View Reserve details 
        private void tsb_ShowDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1FlexGridAvailResrv != null && c1FlexGridAvailResrv.Rows.Count > 1)
                {
                    if (c1FlexGridAvailResrv.RowSel > 0)
                    {
                        OpenReserveForModify(c1FlexGridAvailResrv.RowSel);

                    }
                }
                else
                {
                    MessageBox.Show("Reserve details not available. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void tsb_Modify_Click(object sender, EventArgs e)
        {
            Boolean _IsModified = false;
            try
            {
                if (c1FlexGridChargesClaims.Rows.Count > 1)
                {
                    _IsModified = ModifyCharge();
                    if (_IsModified)
                    {
                        oPatientControl.RefreshData();
                        RemoveTabPageTag();
                        FillClaim_Charges();
                    }
                }
                else
                {
                    MessageBox.Show("Claim not available. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void tsb_ViewPatRefund_Click(object sender, EventArgs e)
        {

            try
            {
                object nrefundid = null;
                Int64 nPatientID = 0;
                if (c1PatientRefund.Rows.Count > 1)
                {
                    nrefundid = c1PatientRefund.GetData(c1PatientRefund.RowSel, 0);
                    nPatientID = Convert.ToInt64(c1PatientRefund.GetData(c1PatientRefund.RowSel, 11));
                    frmPatientPayRefundViewV2 ofrmPatientPayRefundView = new frmPatientPayRefundViewV2(gloPMGlobal.DatabaseConnectionString, nPatientID, Convert.ToInt64(nrefundid));
                    ofrmPatientPayRefundView.ShowDialog(this);
                    ofrmPatientPayRefundView.Dispose();

                    oPatientControl.RefreshBalances();
                    FillPatientReserves();
                    FillPatientRefunds();
                }
                else
                {
                    MessageBox.Show("Refund details are not available.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void tsb_PatRefund_Click(object sender, EventArgs e)
        {
            try
            {
                if (_nPatientID > 0 && _nSelectedPatientId > 0 && _nPAccountId > 0)
                {
                    if (c1FlexGridAvailResrv.Rows.Count > 1)
                    {
                        if (isBadDebtPatient)
                        {
                            DataSet ds = (DataSet)c1FlexGridAvailResrv.DataSource;
                            DataRow[] drCollectionRow = ds.Tables[0].Select("[Collection Agency]<>''");
                            if (drCollectionRow.Length>0)
                            {
                                DialogResult digRes = MessageBox.Show("One of the payments in Reserves is associated with a Collection Agency. Are you sure you want to refund it to patient?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (digRes == DialogResult.No)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Reserves, gloAuditTrail.ActivityType.ReservesDistribution, "NO is selected for \"Collection agency reserves are not refunded to patient\"",_nPatientID,0,0, gloAuditTrail.ActivityOutCome.Success,gloAuditTrail.SoftwareComponent.gloPM);
                                    return;
                                }
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Reserves, gloAuditTrail.ActivityType.ReservesDistribution, "YES is selected for \"Collection agency reserves are refunded to patient\"", _nPatientID,0,0, gloAuditTrail.ActivityOutCome.Success,gloAuditTrail.SoftwareComponent.gloPM);
                            }
                        }
                        
                        frmPatientPaymentRefundV2 ofrmPatientRefundvoid = new frmPatientPaymentRefundV2(gloPMGlobal.DatabaseConnectionString, _nPatientID);
                        
                        //Code added by SaiKrishna date 04-02-2011 for Patient Account Feature.
                        ofrmPatientRefundvoid.PAccountId = _nPAccountId;
                        ofrmPatientRefundvoid.SelectedPatientId = _nSelectedPatientId;
                        ofrmPatientRefundvoid.GuarantorId = _nGuarantorId;
                        ofrmPatientRefundvoid.AccountPatientId = _nAccountPatientId;

                        ofrmPatientRefundvoid.ShowDialog(this);
                        ofrmPatientRefundvoid.Dispose();

                        oPatientControl.RefreshBalances();
                        FillPatientReserves();
                        FillPatientRefunds();

                    }
                    else
                    {
                        MessageBox.Show("Cannot create a patient refund.  Patient refunds are made from available patient reserves. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (_nPatientID == 0 && _nSelectedPatientId == 0 && _nPAccountId > 0)
                {
                    //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
                    gloPatientFinancialViewV2 objPatFinacialView = null;
                    if (oPatientControl.IsAllAccPatSelected)
                    {
                        objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
                    }
                    else
                    {
                        objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
                    }

                    Int64 PatietnID = 0;
                    PatietnID = objPatFinacialView.GetAccountOwnerID();
                    frmPatientPaymentRefundV2 ofrmPatientRefundvoid = new frmPatientPaymentRefundV2(gloPMGlobal.DatabaseConnectionString, PatietnID);

                    //Code added by SaiKrishna date 04-02-2011 for Patient Account Feature.
                    ofrmPatientRefundvoid.PAccountId = _nPAccountId;
                    ofrmPatientRefundvoid.SelectedPatientId = _nSelectedPatientId;
                    ofrmPatientRefundvoid.GuarantorId = _nGuarantorId;
                    ofrmPatientRefundvoid.AccountPatientId = _nAccountPatientId;

                    ofrmPatientRefundvoid.ShowDialog(this);
                    ofrmPatientRefundvoid.Dispose();
                    if (ofrmPatientRefundvoid.DialogResult == DialogResult.OK)
                    {
                        oPatientControl.RefreshBalances();
                        FillPatientReserves();
                        FillPatientRefunds();
                    }

                }
                else
                {
                    MessageBox.Show("Please select the patient.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsbViewHistory_Click(object sender, EventArgs e)
        {
            //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            }

            //if (_nPatientID > 0)
            //{
            try
            {
                Int64 ParamTransactionId = 0;
                Int64 nPatientID = 0;

                if (Convert.ToString(tbPatientFinancial.SelectedTab.Text).ToUpper() == _strTagClaims.ToUpper())
                {
                    if (c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionMSTID"].Index) != null && Convert.ToString(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionMSTID"].Index)) != "")
                    {
                        DataSet dsPatFinView = new DataSet();

                        if (Convert.ToInt32(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-")) >= 0)
                        {
                            if (Convert.ToInt32(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["bIsVoid"].Index)) == 1)
                            {
                                ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(0, c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-"))), c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-") + 1), true);
                            }
                            else
                            {
                                Boolean chkVoid = objPatFinacialView.ChkClaimVoided(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionMSTID"].Index)));
                                if (chkVoid == false)
                                    ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(0, c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-"))), c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-") + 1), false);
                                else
                                    ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(0, c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-"))), c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-") + 1), true);
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["bIsVoid"].Index)) == 1)
                            {
                                ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)), "", true);
                            }
                            else
                            {
                                Boolean chkVoid = objPatFinacialView.ChkClaimVoided(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionMSTID"].Index)));
                                if (chkVoid == false)
                                    ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)), "", false);
                                else
                                    ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)), "", true);

                            }
                        }
                        //frmClaimChargeHistory ofrmClaimChargeHistory = new frmClaimChargeHistory( gloPMGlobal.DatabaseConnectionString, _nPatientID, gloPMGlobal.ClinicID, ParamTransactionId);

                        // start abhisekh 07 sept 2010
                        //to get latest Transaction ID

                        gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");
                        Int64 mainTransactionID = 0;
                        if (ParamTransactionId != 0)
                            mainTransactionID = ogloBilling.GetLastTransactionID(ParamTransactionId);

                        nPatientID = Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["PatientID"].Index));
                        //frmClaimChargeHistory ofrmClaimChargeHistory = new frmClaimChargeHistory( gloPMGlobal.DatabaseConnectionString, _nPatientID, gloPMGlobal.ClinicID, ParamTransactionId);
                        gloBilling.frmClaimChargeHistoryV2 ofrmClaimChargeHistory = new gloBilling.frmClaimChargeHistoryV2(gloPMGlobal.DatabaseConnectionString, nPatientID, gloPMGlobal.ClinicID, mainTransactionID);

                        // end abhisekh 07 sept 2010
                        ofrmClaimChargeHistory.StartPosition = FormStartPosition.CenterScreen;
                        ofrmClaimChargeHistory.ShowDialog(this);
                        ofrmClaimChargeHistory.Dispose();
                        ogloBilling.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            finally
            { objPatFinacialView.Dispose(); }
            //}
        }

        private void tsbPatientPmnt_Click(object sender, EventArgs e)
        {
            try
            {

                //if (_nPatientID > 0)
                //{
                Int64 eobPaymentId = 0;
                Int64 nPatientID = 0;
                if (Convert.ToString(tbPatientFinancial.SelectedTab.Text).ToUpper() == _strTagPaymentReserves.ToUpper())
                {
                    if (c1FlexGridPmnt.Rows.Count > 1)
                    {
                        if (c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nEOBPaymentID"].Index)) != "")
                        {
                            nPatientID = Convert.ToInt64(c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nPayerID"].Index));
                            eobPaymentId = Convert.ToInt64(c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nEOBPaymentID"].Index));
                            frmViewPatientPaymentV2 ofrmViewPatientPayment = new frmViewPatientPaymentV2(gloPMGlobal.DatabaseConnectionString, nPatientID, gloPMGlobal.ClinicID, eobPaymentId);
                            ofrmViewPatientPayment.StartPosition = FormStartPosition.CenterScreen;
                            ofrmViewPatientPayment.ShowDialog(this);
                            ofrmViewPatientPayment.Dispose();
                           
                            oPatientControl.RefreshBalances();
                            RemoveTabPageTag();
                            FillPatientPayment();
                            FillPatientRefunds();
                            FillPatientReserves();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Payment details are not available. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                //}
                //else
                //{
                //    MessageBox.Show("Please select the patient.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
        }

        

        //Added By Shweta -20100812
        private void tsb_ViewStmt_Click(object sender, EventArgs e)
        {

            if (c1FlexGrid_Statements.Rows.Count > 1)
            {
                if (c1FlexGrid_Statements.RowSel < c1FlexGrid_Statements.Rows.Count)
                {
                    if (c1FlexGrid_Statements.Rows.Selected != null)
                    {
                        if (c1FlexGrid_Statements.RowSel > 0)
                        {
                            _nTrasactionID = Convert.ToInt64(c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, 8));
                            //_PatientID_To_Delete = Convert.ToInt64(c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, COL_nPatientID));
                        }
                    }
                }
                if (c1FlexGrid_Statements.RowSel < c1FlexGrid_Statements.Rows.Count)
                {
                    if (c1FlexGrid_Statements.Rows.Selected != null)
                    {
                        if (c1FlexGrid_Statements.RowSel > 0)
                        {
                            if (_nTrasactionID > 0)
                            {
                                ViewPatientStatementTemplate();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Statement details are not available.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Added By Shweta -20100812
        private void tsbPatientStmtNotes_Click(object sender, EventArgs e)
        {
            if (_nPatientID > 0)
            {
                gloBilling.frmPatientStatementNotes frmPSN = new gloBilling.frmPatientStatementNotes(gloPMGlobal.DatabaseConnectionString, _nPatientID);
                try
                {
                    //frmPSN.AccountPatientID = _nAccountPatientId;
                    //frmPSN.PAccountID = _nPAccountId;
                    frmPSN.ShowDialog(this);
                    frmPSN.Dispose();
                    tsb_Refresh_Click(null, null);
                    frmPSN = null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
                }
            }
        }

        //Added By Shweta 20100812
        private void tsb_btnNewStatement_Click(object sender, EventArgs e)
        {
            //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            gloPatient.gloAccount objgloAccount = null;
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            }


            //gloBilling.Statement.frmRpt_Revised_PatientStatement frmPatStmt = new global::gloBilling.Statement.frmRpt_Revised_PatientStatement( gloPMGlobal.DatabaseConnectionString, _nPatientID, _IsPatientFinancialView);
            if (_nPatientID == 0 && _nSelectedPatientId == 0 && _nPAccountId != 0)
            {
                Int64 nPatientID = 0;
                nPatientID = objPatFinacialView.GetAccountOwnerID();
                gloBilling.Statement.frmRpt_Revised_PatientStatement frmPatStmt = new global::gloBilling.Statement.frmRpt_Revised_PatientStatement(gloPMGlobal.DatabaseConnectionString, nPatientID, _nPAccountId, true);
                frmPatStmt.on_FromClose += new global::gloBilling.Statement.frmRpt_Revised_PatientStatement.onFromClose(frmPatStmt_on_FromClose);
                frmPatStmt.WindowState = FormWindowState.Maximized;
                objgloAccount = new gloPatient.gloAccount(gloPMGlobal.DatabaseConnectionString);
                frmPatStmt.IsPatientAccountEnable = objgloAccount.GetPatientAccountFeatureSetting();
                if (oPatientControl.IsAllAccPatSelected)
                {
                    frmPatStmt.IsAllAccPatSelected = true;
                }
                if (!IsCalledFromInsPmt)
                { frmPatStmt.IsCalledFromPatAcct = true; frmPatStmt.MdiParent = this.ParentForm; frmPatStmt.Show(); frmPatStmt.generateIndividualStmt(); }
                else
                { frmPatStmt.IsCalledFromPatAcct = true; frmPatStmt.VisibleChanged += new EventHandler(frmPatStmtVisibleChanged); frmPatStmt.ShowDialog(this); frmPatStmt.VisibleChanged -= new EventHandler(frmPatStmtVisibleChanged); frmPatStmt.Dispose(); frmPatStmt = null; }
                if (frmPatStmt != null)
                {
                    foreach (Form myForm in Application.OpenForms)
                    {

                        if (myForm.TopMost)
                        {
                            myForm.TopMost = false;
                        }

                    }
                    frmPatStmt.TopMost = true;
                }
            }
            else if (_nPatientID != 0 && _nSelectedPatientId != 0 && _nPAccountId != 0)
            {

                gloBilling.Statement.frmRpt_Revised_PatientStatement frmPatStmt = new global::gloBilling.Statement.frmRpt_Revised_PatientStatement(gloPMGlobal.DatabaseConnectionString, _nPatientID, _nPAccountId, true);
                frmPatStmt.on_FromClose += new global::gloBilling.Statement.frmRpt_Revised_PatientStatement.onFromClose(frmPatStmt_on_FromClose);
                frmPatStmt.WindowState = FormWindowState.Maximized;
                objgloAccount = new gloPatient.gloAccount(gloPMGlobal.DatabaseConnectionString);
                frmPatStmt.IsPatientAccountEnable = objgloAccount.GetPatientAccountFeatureSetting();
                if (oPatientControl.IsAllAccPatSelected)
                {
                    frmPatStmt.IsAllAccPatSelected = true;
                }
                if (!IsCalledFromInsPmt)
                { frmPatStmt.IsCalledFromPatAcct = true; frmPatStmt.MdiParent = this.ParentForm; frmPatStmt.Show(); frmPatStmt.generateIndividualStmt(); }
                else
                { frmPatStmt.IsCalledFromPatAcct = true; frmPatStmt.IsCalledFromInsPmt = IsCalledFromInsPmt; frmPatStmt.ShowDialog(this); frmPatStmt.Dispose(); frmPatStmt = null; }
                if (frmPatStmt != null)
                {
                    foreach (Form myForm in Application.OpenForms)
                    {

                        if (myForm.TopMost)
                        {
                            myForm.TopMost = false;
                        }

                    }
                    frmPatStmt.TopMost = true;
                }
            }

        }

        private void frmPatStmtVisibleChanged(object sender, EventArgs e)
        {
            frmRpt_Revised_PatientStatement frmPatStmt = sender as frmRpt_Revised_PatientStatement;
            if (frmPatStmt != null && frmPatStmt.Visible)
            {
                frmPatStmt.generateIndividualStmt();
            }
        }
        private void tbVoidStatment_Click(object sender, EventArgs e)
        {
           // gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            } 

            try
            {
                if (c1FlexGrid_Statements.Rows.Count > 1)
                {
                    DataSet dsPatFinView = new DataSet();
                    Int64 MasterId = 0;
                    Int64 DetailId = 0;
                    String Status = "";
                    if (c1FlexGrid_Statements.RowSel < c1FlexGrid_Statements.Rows.Count)
                    {
                        if (c1FlexGrid_Statements.Rows.Selected != null)
                        {
                            if (c1FlexGrid_Statements.RowSel > 0)
                            {
                                //_nTrasactionID = Convert.ToInt64(c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, COL_nTransactionID));
                                DetailId = Convert.ToInt64(c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, 12));
                                MasterId = Convert.ToInt64(c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, 7));
                                Status = (c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, 10)).ToString();
                                //_PatientID_To_Delete = Convert.ToInt64(c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, COL_nPatientID));
                            }
                        }
                    }
                    if (Status != "Voided")
                    {
                        DialogResult dlgRst = MessageBox.Show("Do you want to void the statement? ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dlgRst == DialogResult.Yes)
                        {
                            //frmVoidStatmentBatch Objfrm = new frmVoidStatmentBatch(MasterId, DetailId, false);
                            //Objfrm.ShowDialog();

                            gloBilling.frmVoidStatmentBatch Objfrm = new gloBilling.frmVoidStatmentBatch(MasterId, DetailId, false);
                            Objfrm.PAccountID = _nPAccountId;
                            Objfrm.ShowDialog(this);
                            objPatFinacialView.Fill_PatientStatement(out dsPatFinView);
                            dsPatFinView.Tables[0].TableName = "Statement";
                            this.c1FlexGrid_Statements.DataMember = "Statement";
                            this.c1FlexGrid_Statements.DataSource = dsPatFinView;

                            c1FlexGrid_Statements.Cols[13].DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                            c1FlexGrid_Statements.Cols[13].ComboList = "...";
                            c1FlexGrid_Statements.Cols[13].AllowEditing = true;
                            c1FlexGrid_Statements.Cols[13].ImageAlign = ImageAlignEnum.CenterCenter;

                            if (c1FlexGrid_Statements != null && c1FlexGrid_Statements.Cols.Count > 0)
                            {
                                for (int colInd = 0; colInd < c1FlexGrid_Statements.Cols.Count; colInd++)
                                {
                                    if (c1FlexGrid_Statements.Cols[colInd].Name != "Note")
                                    {
                                        c1FlexGrid_Statements.Cols[colInd].AllowEditing = false;
                                    }
                                }
                            }
                            if (c1FlexGrid_Statements != null && c1FlexGrid_Statements.Rows.Count > 0)
                            {
                                for (int RowInd = 1; RowInd < c1FlexGrid_Statements.Rows.Count; RowInd++)
                                {
                                    if (Convert.ToString(c1FlexGrid_Statements.Rows[RowInd]["Status"].ToString()) != "Voided")
                                    {
                                        c1FlexGrid_Statements.Rows[RowInd].AllowEditing = false;
                                    }
                                }
                            }

                            this.c1FlexGrid_Statements.Focus();
                            Objfrm.Dispose();
                            Objfrm = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Statement is already voided.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Statement details are not available.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {

                if (objPatFinacialView != null)
                {
                    objPatFinacialView.Dispose();
                }
            }
        }

        private void tsb_GlobalPeriods_Click(object sender, EventArgs e)
        {
            gloBilling.frmSetupGlobalPeriod ofrmSetupGlobalPeriod = new gloBilling.frmSetupGlobalPeriod(oPatientControl.CmbSelectedPatientID);
            ofrmSetupGlobalPeriod.ShowDialog(this);
            ofrmSetupGlobalPeriod.Dispose();
            tbpgGlobalPeriods.Tag = "";
            FillGlobalPeriods();
        }

        private void tsb_ModifyGlobalPeriods_Click(object sender, EventArgs e)
        {
            if (c1GlobalPeriods.Rows.Count > 1)
            {
                gloBilling.frmSetupModifyGlobalPeriod ofrmSetupGlobalPeriod = new gloBilling.frmSetupModifyGlobalPeriod(Convert.ToInt64(c1GlobalPeriods.GetData(c1GlobalPeriods.RowSel, "ID")));
                ofrmSetupGlobalPeriod.ShowDialog(this);
                ofrmSetupGlobalPeriod.Dispose();
                tbpgGlobalPeriods.Tag = "";
                FillGlobalPeriods();
            }
            else
            {
                MessageBox.Show("Global periods are not available.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsb_DeleteGlobalPeriod_Click(object sender, EventArgs e)
        {
            if (c1GlobalPeriods.Rows.Count > 1)
            {
                gloBilling.clsGlobalPeriods objGlobalPeriods = new gloBilling.clsGlobalPeriods();

                try
                {
                    if (c1GlobalPeriods.RowSel > 0)
                    {
                        DialogResult _dlgRslt = DialogResult.None;
                        _dlgRslt = MessageBox.Show("Are you sure you want to delete selected record?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                        switch (_dlgRslt)
                        {
                            case DialogResult.Yes:
                                objGlobalPeriods.DeleteGlobalPeriod(Convert.ToInt64(c1GlobalPeriods.GetData(c1GlobalPeriods.RowSel, "ID")));
                                tbpgGlobalPeriods.Tag = "";
                                FillGlobalPeriods();
                                break;
                            default:
                                break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                }
                finally
                {
                    if (objGlobalPeriods != null) { objGlobalPeriods.Dispose(); }
                }
            }
            else
            {
                MessageBox.Show("Global periods are not available.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void tsp_AccNotes_Click(object sender, EventArgs e)
        {
            gloPatient.frmAccountNotes ofrmAccountNotes = new gloPatient.frmAccountNotes();
            ofrmAccountNotes.nPatientAccountID = oPatientControl.AccountPatientID;
            ofrmAccountNotes.nPatientID = oPatientControl.PatientID;
            ofrmAccountNotes.nPAccountID = oPatientControl.PAccountID;

            ofrmAccountNotes.ShowDialog(this);
            oPatientControl.RefreshData();
            tsb_Refresh_Click(null, null);
            ofrmAccountNotes.Dispose();          
        }

        private void tsbPaymentPlan_Click(object sender, EventArgs e)
        {
            gloBilling.Collections.frmSetupPaymentPlan ofrmSetupPaymentPlan = new gloBilling.Collections.frmSetupPaymentPlan();
            ofrmSetupPaymentPlan.nPAccountID = oPatientControl.PAccountID;
            ofrmSetupPaymentPlan.nPatientAccountID = oPatientControl.AccountPatientID;
            ofrmSetupPaymentPlan.nPatientID = oPatientControl.PatientID;

            ofrmSetupPaymentPlan.ShowDialog(this);
            oPatientControl.RefreshData();
            tsb_Refresh_Click(null, null);
            ofrmSetupPaymentPlan.Dispose(); 
        }

        private void tsp_ViewLog_Click(object sender, EventArgs e)
        {
            ApplyDrillDown();
        }

        #endregion

        #region 'Radio button Event'
        private void rbtn_Claim_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtn_Claim.Checked)
            {
                this.rbtn_Claim.Font = this._fBold;
                FillClaim_Charges();
            }
            else
            {
                this.rbtn_Claim.Font = this._fRegular;
            }
            tbPatientFinancial_SelectedIndexChanged(sender, e);
        }

        private void rbtn_DOS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtn_DOS.Checked)
            {
                //for (int i = 1; i <= c1FlexGridChargesClaims.Rows.Count - 1; i++)
                //{
                //    setNormalGridStyle(i);
                //}
                this.rbtn_DOS.Font = this._fBold;
                DataView _dv = null;
                _dv = (DataView)c1FlexGridChargesClaims.DataSource;
                if (_dv != null)
                {
                    _dv.Sort = "nDOS DESC,SortClaim Desc,SortSubClaim ASC,nTransactionLineNo";
                    //c1FlexGridChargesClaims.DataSource = null;
                    c1FlexGridChargesClaims.DataSource = _dv;
                }
                SetNoteFlag();
                FillClaimOnHold();
            }
            else
            {
                this.rbtn_DOS.Font = this._fRegular;
            }
        }

        private void rbtn_Claim_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.rbtn_Claim.Checked)
            {
                //for (int i = 1; i <= c1FlexGridChargesClaims.Rows.Count - 1; i++)
                //{
                //    setNormalGridStyle(i);
                //}
                this.rbtn_Claim.Font = this._fBold;
                DataView _dv = null;
                _dv = (DataView)c1FlexGridChargesClaims.DataSource;
                if (_dv != null)
                {
                    _dv.Sort = "SortClaim Desc,SortSubClaim ASC, nTransactionLineNo";
                    c1FlexGridChargesClaims.DataSource = _dv;
                }
                SetNoteFlag();
                FillClaimOnHold();
            }
            else
            {
                this.rbtn_Claim.Font = this._fRegular;

            }
        }


        private void rbtn_Responsibility_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtn_Responsibility.Checked)
            {
                this.rbtn_Responsibility.Font = this._fBold;
                FillSummary();
            }
            else
            {
                this.rbtn_Responsibility.Font = this._fRegular;
            }
        }

        private void rbtn_DOS_Summary_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtn_DOS_Summary.Checked)
            {
                this.rbtn_DOS_Summary.Font = this._fBold;
                FillSummary();
            }
            else
            {
                this.rbtn_DOS_Summary.Font = this._fRegular;
            }

        }
        #endregion

        #region "Commented Code By Shweta - 20112306 "
        //private void tbPatientFinancial_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DBLayer oDB = new DBLayer(this. gloPMGlobal.DatabaseConnectionString);
        //    DBParameters oParameters = new DBParameters();
        //    DataSet dsPatFinView = new DataSet();

        //    //Code added by SaiKrishna for account feature
        //    DataTable dtAccPatSummary = new DataTable("Summary");
        //    DataSet dsAccPatSummary = new DataSet();

        //    try
        //    {
        //        try
        //        {
        //            int cntr;
        //            DataTable dtTotalReserves;
        //            //  Exception ex;
        //            if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Patient_Payment_and_Reserves))
        //            {
        //                tsbViewHistory.Visible = false;
        //                tsb_ShowDetails.Visible = true;
        //                //ts_VoidPayment.Visible = true;
        //                tsbPatientPmnt.Visible = true;
        //                tsbPatientPmnt.Enabled = true;
        //                tsb_Modify.Visible = false;
        //                //ts_VoidPayment.Enabled = true;
        //                tsb_ShowZeroBalance.Visible = false;
        //                tsb_HideZeroBalance.Visible = false;
        //                tsb_PatRefund.Visible = true;
        //                tsb_ViewPatRefund.Visible = true;
        //                tsb_PatRefund.Enabled = true;
        //                tsb_ViewPatRefund.Enabled = false;
        //                tsb_ViewStmt.Visible = false;
        //                tsbPatientStmtNotes.Visible = false;
        //                tsb_btnNewStatement.Visible = false;
        //                tbVoidStatment.Visible = false;
        //                FillPatientRefund();


        //                //tsb_ShowHideZeroBalance.Text = " Show Zero Balance";
        //                DataSet dsPayment = new DataSet();
        //                try
        //                {
        //                    //Code changed by SaiKrishna for procedure name
        //                    dsPatFinView = this.fillgridData("PA_Patient_Financial_View_Reserve_V2", oParameters, oDB);
        //                    dsPayment = this.fillgridData("PA_Patient_Financial_View_PatientPayment_V2", oParameters, oDB);

        //                    dsPatFinView.Tables[0].TableName = "Reserves";
        //                    dsPayment.Tables[0].TableName = "Payments";
        //                    if (dsPayment.Tables["Payments"].Rows.Count > 0)
        //                    {

        //                        DataView dv = dsPayment.Tables["Payments"].DefaultView;
        //                        DataTable dtUniqueData = dv.ToTable(true, "nEOBPaymentID");
        //                        DataTable dtFilterData;
        //                        dtFilterData = dsPayment.Tables["Payments"].Clone();
        //                        for (cntr = 0; cntr <= dtUniqueData.Rows.Count - 1; cntr++)
        //                        {
        //                            DataRow[] resultRows = null;
        //                            resultRows = dsPayment.Tables["Payments"].Select("nEOBPaymentID=" + dtUniqueData.Rows[cntr]["nEOBPaymentID"] + " and nPaymentNoteType=6");
        //                            if (resultRows.Length > 0)
        //                            {
        //                                foreach (DataRow dr in resultRows)
        //                                {
        //                                    dtFilterData.ImportRow(dr);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                resultRows = dsPayment.Tables["Payments"].Select("nEOBPaymentID=" + dtUniqueData.Rows[cntr]["nEOBPaymentID"]);
        //                                foreach (DataRow dr in resultRows)
        //                                {
        //                                    dtFilterData.ImportRow(dr);
        //                                }
        //                            }
        //                        }
        //                        if (dtFilterData.Rows.Count > 0)
        //                        {
        //                            dtFilterData.TableName = "Payments";
        //                            dsPayment.Tables.Clear();
        //                            dsPayment.Tables.Add(dtFilterData);
        //                        }
        //                    }

        //                    this.c1FlexGridAvailResrv.DataMember = "Reserves";
        //                    this.c1FlexGridAvailResrv.DataSource = dsPatFinView;
        //                    c1FlexGridAvailResrv.ShowCellLabels = false;
        //                    this.c1FlexGridPmnt.DataMember = "Payments";
        //                    this.c1FlexGridPmnt.DataSource = dsPayment;
        //                    c1FlexGridPmnt.ShowCellLabels = false;
        //                    //decimal decToReserves = 0;
        //                    decimal decAvailable = 0;
        //                    if (dsPatFinView.Tables["Reserves"].Rows.Count > 0)
        //                    {
        //                        tsb_PatRefund.Enabled = true;
        //                        for (cntr = 0; cntr <= (dsPatFinView.Tables["Reserves"].Rows.Count - 1); cntr++)
        //                        {
        //                            //decToReserves += Convert.ToDecimal(dsPatFinView.Tables["Reserves"].Rows[cntr]["nAmount"] == DBNull.Value ? 0 : dsPatFinView.Tables["Reserves"].Rows[cntr]["nAmount"]);
        //                            decAvailable += Convert.ToDecimal(dsPatFinView.Tables["Reserves"].Rows[cntr]["AvailableReserve"] == DBNull.Value ? 0 : dsPatFinView.Tables["Reserves"].Rows[cntr]["AvailableReserve"]);
        //                        }

        //                        //this.c1FlexGridTotalAvailResrv.Visible = true;
        //                        //pnlTotalRsrv.BackColor = Color.FromArgb(207, 224, 248);

        //                        dtTotalReserves = new DataTable();
        //                        dtTotalReserves = dsPatFinView.Tables["Reserves"].Clone();
        //                        dtTotalReserves.TableName = "TotalReserves";
        //                        dtTotalReserves.Columns["nCloseDate"].DataType = System.Type.GetType("System.String");
        //                        dtTotalReserves.ImportRow(dsPatFinView.Tables["Reserves"].Rows[dsPatFinView.Tables["Reserves"].Rows.Count - 1]);
        //                        dsPatFinView.Tables.Add(dtTotalReserves);
        //                        //dsPatFinView.Tables["TotalReserves"].Columns["nCloseDate"].DataType = System.Type.GetType("System.String");
        //                        dsPatFinView.Tables["TotalReserves"].Rows[0]["OriginalAmount"] = "";

        //                        dsPatFinView.Tables["TotalReserves"].Rows[0]["nAmount"] = DBNull.Value;
        //                        dsPatFinView.Tables["TotalReserves"].Rows[0]["AvailableReserve"] = decAvailable;
        //                        dsPatFinView.Tables["TotalReserves"].Rows[0]["nPaymentNoteSubType"] = DBNull.Value;
        //                        dsPatFinView.Tables["TotalReserves"].Rows[0]["sNoteDescription"] = "";
        //                        dsPatFinView.Tables["TotalReserves"].Rows[0]["nCloseDate"] = "Total :";

        //                        //Code Added by SaiKrishna
        //                        dsPatFinView.Tables["TotalReserves"].Rows[0]["sPatientName"] = "";

        //                        this.c1FlexGridTotalAvailResrv.Visible = true;
        //                        this.c1FlexGridTotalAvailResrv.Rows[0].Visible = false;

        //                        this.c1FlexGridTotalAvailResrv.DataMember = "TotalReserves";
        //                        this.c1FlexGridTotalAvailResrv.DataSource = dsPatFinView;
        //                        setGridStyle(c1FlexGridTotalAvailResrv, c1FlexGridTotalAvailResrv.Rows.Count - 1, 11, 25);
        //                        if (dsPatFinView.Tables["Reserves"].Rows.Count >= iReserveSelRow)
        //                            c1FlexGridAvailResrv.Row = iReserveSelRow;
        //                    }
        //                    else
        //                    {

        //                        this.c1FlexGridTotalAvailResrv.DataMember = "Reserves";
        //                        this.c1FlexGridTotalAvailResrv.DataSource = dsPatFinView;
        //                        this.c1FlexGridTotalAvailResrv.Visible = false;


        //                    }
        //                    if (dsPayment.Tables["Payments"].Rows.Count > 0)
        //                    {
        //                        //ts_VoidPayment.Enabled = true;

        //                        tsbPatientPmnt.Enabled = true;
        //                        decAvailable = 0;
        //                        for (cntr = 0; cntr <= (dsPayment.Tables["Payments"].Rows.Count - 1); cntr++)
        //                        {
        //                            decAvailable += Convert.ToDecimal(dsPayment.Tables["Payments"].Rows[cntr]["nCheckAmount"] == DBNull.Value ? 0 : dsPayment.Tables["Payments"].Rows[cntr]["nCheckAmount"]);
        //                        }
        //                        DataTable dtTotalPayment = new DataTable();
        //                        dtTotalPayment = dsPayment.Tables["Payments"].Clone();
        //                        dtTotalPayment.TableName = "TotalPayments";
        //                        dtTotalPayment.ImportRow(dsPayment.Tables["Payments"].Rows[dsPayment.Tables["Payments"].Rows.Count - 1]);
        //                        dsPayment.Tables.Add(dtTotalPayment);
        //                        dsPayment.Tables["TotalPayments"].Rows[0]["nCloseDate"] = "Total :";
        //                        dsPayment.Tables["TotalPayments"].Rows[0]["sPaymentTrayDescription"] = "";
        //                        dsPayment.Tables["TotalPayments"].Rows[0]["nPaymentMode"] = "";
        //                        dsPayment.Tables["TotalPayments"].Rows[0]["nCheckDate"] = DBNull.Value;
        //                        dsPayment.Tables["TotalPayments"].Rows[0]["sCheckNumber"] = "";
        //                        dsPayment.Tables["TotalPayments"].Rows[0]["nCheckAmount"] = decAvailable;
        //                        dsPayment.Tables["TotalPayments"].Rows[0]["sNoteDescription"] = "";
        //                        dsPayment.Tables["TotalPayments"].Rows[0]["Status"] = "";

        //                        //Code Added by SaiKrishna
        //                        dsPayment.Tables["TotalPayments"].Rows[0]["sPatientName"] = "";

        //                        this.c1FlexGridPmntTotal.Visible = true;
        //                        this.c1FlexGridPmntTotal.Rows[0].Visible = false;

        //                        this.c1FlexGridPmntTotal.DataMember = "TotalPayments";
        //                        this.c1FlexGridPmntTotal.DataSource = dsPayment;
        //                        setGridStyle(c1FlexGridPmntTotal, c1FlexGridPmntTotal.Rows.Count - 1, 8, 14);
        //                        if (dsPayment.Tables["Payments"].Rows.Count >= iPaymentSelRow)
        //                            c1FlexGridPmnt.Row = iPaymentSelRow;
        //                    }
        //                    else
        //                    {


        //                        this.c1FlexGridPmntTotal.DataMember = "Payments";
        //                        this.c1FlexGridPmntTotal.DataSource = dsPayment;
        //                        this.c1FlexGridPmntTotal.Visible = false;
        //                    }
        //                }
        //                catch (Exception ex1)
        //                {
        //                    ex1.ToString();


        //                }
        //            }
        //            else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Claim_Details))
        //            {

        //                tsb_ShowDetails.Enabled = true;
        //                //ts_VoidPayment.Enabled = true;
        //                tsbPatientPmnt.Visible = false;
        //                tsbPatientPmnt.Enabled = true;
        //                tsb_ShowDetails.Visible = false;
        //                tsb_PatRefund.Visible = false;
        //                tsb_ViewPatRefund.Visible = false;
        //                tsb_ViewStmt.Visible = false;
        //                tsbPatientStmtNotes.Visible = false;
        //                tsb_btnNewStatement.Visible = false;
        //                tbVoidStatment.Visible = false;
        //                if (_blnShowZeroBal == true)
        //                {
        //                    tsb_HideZeroBalance.Visible = false;
        //                    tsb_ShowZeroBalance.Visible = true;
        //                }
        //                else
        //                {
        //                    tsb_HideZeroBalance.Visible = true;
        //                    tsb_ShowZeroBalance.Visible = false;
        //                }
        //                //ts_VoidPayment.Visible = false;
        //                tsb_Modify.Visible = true;
        //                short sZeroFlag = 0;
        //                int sSort = 1;

        //                if (tsb_ShowZeroBalance.Visible == true)
        //                    sZeroFlag = 0;
        //                else
        //                    sZeroFlag = 1;

        //                if (this.rbtn_Claim.Checked)
        //                {
        //                    sSort = 1;
        //                }
        //                else if (this.rbtn_DOS.Checked)
        //                {
        //                    sSort = 2;
        //                }

        //                try
        //                {
        //                    //Code changed by SaiKrishna for Account Feature.Proc changed and accountID parameter added.
        //                    oParameters.Add("@nPAccountID", _nPAccountId, ParameterDirection.Input, SqlDbType.BigInt);
        //                    oParameters.Add("@nPatientID", _nSelectedPatientId, ParameterDirection.Input, SqlDbType.BigInt);
        //                    oParameters.Add("@nClinicID", this.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //                    oParameters.Add("@ZeroBalFlag", sZeroFlag, ParameterDirection.Input, SqlDbType.Bit);
        //                    oParameters.Add("@SortByFlag", sSort, ParameterDirection.Input, SqlDbType.Int);
        //                    oDB.Connect(false);
        //                    oDB.Retrive("PA_Patient_Financial_View_Claims_Charges", oParameters, out dsPatFinView);
        //                    oDB.Disconnect();
        //                    dsPatFinView.Tables[0].TableName = "Claims_Charges";
        //                    this.c1FlexGridChargesClaims.DataMember = "Claims_Charges";
        //                    this.c1FlexGridChargesClaims.DataSource = dsPatFinView;
        //                    FillClaimOnHold();
        //                    decimal decAmount = 0;
        //                    decimal decAdjs = 0;
        //                    decimal decInsPmnt = 0;
        //                    decimal decPatPmnt = 0;
        //                    decimal decInsPending = 0;
        //                    decimal decPatPending = 0;
        //                    if (dsPatFinView.Tables["Claims_Charges"].Rows.Count > 0)
        //                    {
        //                        tsbViewHistory.Visible = true;
        //                        for (cntr = 0; cntr <= (dsPatFinView.Tables["Claims_Charges"].Rows.Count - 1); cntr++)
        //                        {
        //                            decAmount += Convert.ToDecimal(dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["dTotal"] == DBNull.Value ? 0 : dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["dTotal"]);
        //                            decAdjs += Convert.ToDecimal(dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["PreviousAdjustment"] == DBNull.Value ? 0 : dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["PreviousAdjustment"]);
        //                            decInsPmnt += Convert.ToDecimal(dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["InsurancePayment"] == DBNull.Value ? 0 : dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["InsurancePayment"]);
        //                            decPatPmnt += Convert.ToDecimal(dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["PatientPayment"] == DBNull.Value ? 0 : dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["PatientPayment"]);
        //                            decInsPending += Convert.ToDecimal(dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["InsurancePending"] == DBNull.Value ? 0 : dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["InsurancePending"]);
        //                            decPatPending += Convert.ToDecimal(dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["PatientDue"] == DBNull.Value ? 0 : dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["PatientDue"]);
        //                            if (Convert.ToBoolean(dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["blnNoteFlag"] == DBNull.Value ? 0 : dsPatFinView.Tables["Claims_Charges"].Rows[cntr]["blnNoteFlag"]))
        //                            {
        //                                System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Notes;
        //                                this.c1FlexGridChargesClaims.SetCellImage(cntr + 1, COL_NOTE_IMAGE, imgFlag);
        //                            }
        //                            if (dsPatFinView.Tables["Claims_Charges"].Rows.Count >= iChargesSelRow)
        //                                c1FlexGridChargesClaims.Row = iChargesSelRow;

        //                        }
        //                        dtTotalReserves = new DataTable();
        //                        dtTotalReserves = dsPatFinView.Tables["Claims_Charges"].Clone();
        //                        dtTotalReserves.TableName = "TotalClaims_Charges";
        //                        dtTotalReserves.ImportRow(dsPatFinView.Tables["Claims_Charges"].Rows[dsPatFinView.Tables["Claims_Charges"].Rows.Count - 1]);
        //                        dsPatFinView.Tables.Add(dtTotalReserves);
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["dTotal"] = decAmount;
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["PreviousAdjustment"] = decAdjs;
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["InsurancePayment"] = decInsPmnt;
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["PatientPayment"] = decPatPmnt;
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["InsurancePending"] = decInsPending;
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["PatientDue"] = decPatPending;
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["SplitClaimNumber"] = "Total :";
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["DOS"] = DBNull.Value;
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["sCPTCode"] = "";
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["sMod1Code"] = "";
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["sMod2Code"] = "";
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["sDx1Code"] = "";
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["sDx2Code"] = "";
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["sDx3Code"] = "";
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["sDx4Code"] = "";
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["nProviderName"] = "";
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["nCloseDate"] = DBNull.Value;
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["Party"] = "";
        //                        dsPatFinView.Tables["TotalClaims_Charges"].Rows[0]["sPatientName"] = "";
        //                        this.c1FlexGridTotalChargesClaims.Rows[0].Visible = false;
        //                        this.c1FlexGridTotalChargesClaims.DataMember = "TotalClaims_Charges";
        //                        this.c1FlexGridTotalChargesClaims.DataSource = dsPatFinView;
        //                        setGridStyle(c1FlexGridTotalChargesClaims, c1FlexGridTotalChargesClaims.Rows.Count - 1, 12, c1FlexGridTotalChargesClaims.Cols.Count - 1);
        //                    }
        //                    else
        //                    {

        //                        tsbViewHistory.Visible = false;
        //                        this.c1FlexGridTotalChargesClaims.DataMember = "Claims_Charges";
        //                        this.c1FlexGridTotalChargesClaims.DataSource = dsPatFinView;
        //                        this.c1FlexGridTotalChargesClaims.Rows[0].Visible = false;

        //                    }
        //                }
        //                catch (Exception ex2)
        //                {

        //                    ex2.ToString();
        //                    this.c1FlexGridTotalChargesClaims.Rows[0].Visible = false;
        //                }
        //            }
        //            else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Chronology))
        //            {
        //                tsbViewHistory.Visible = false;
        //                tsb_ShowDetails.Enabled = true;
        //                //ts_VoidPayment.Enabled = true;
        //                tsb_ShowDetails.Visible = false;
        //                tsbPatientPmnt.Visible = false;
        //                tsbPatientPmnt.Enabled = true;
        //                tsb_ShowZeroBalance.Visible = false;
        //                tsb_HideZeroBalance.Visible = false;
        //                //ts_VoidPayment.Visible = false;
        //                tsb_Modify.Visible = false;
        //                tsb_PatRefund.Visible = false;
        //                tsb_ViewPatRefund.Visible = false;
        //                tsb_ViewStmt.Visible = false;
        //                tsbPatientStmtNotes.Visible = false;
        //                tsb_btnNewStatement.Visible = false;
        //                tbVoidStatment.Visible = false;
        //                //tsb_ShowHideZeroBalance.Text=" Show Zero Balance";
        //                try
        //                {
        //                    dsPatFinView = this.fillgridData("gSP_GET_Chronology", oParameters, oDB);
        //                    dsPatFinView.Tables[0].TableName = "Chronology";
        //                    this.c1FlexGridChronology.DataMember = "Chronology";
        //                    this.c1FlexGridChronology.DataSource = dsPatFinView;

        //                }
        //                catch (Exception ex3)
        //                {
        //                    ex3.ToString();
        //                }
        //            }
        //            else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Insurance_Remittance))
        //            {
        //                tsbViewHistory.Visible = false;
        //                tsb_ShowDetails.Enabled = true;
        //                //ts_VoidPayment.Enabled = true;
        //                tsb_ShowDetails.Visible = false;
        //                tsb_ShowZeroBalance.Visible = false;
        //                tsb_HideZeroBalance.Visible = false;
        //                //ts_VoidPayment.Visible = false;
        //                tsbPatientPmnt.Visible = false;
        //                tsbPatientPmnt.Enabled = true;
        //                tsb_Modify.Visible = false;
        //                tsb_PatRefund.Visible = false;
        //                tsb_ViewPatRefund.Visible = false;
        //                tsb_ViewStmt.Visible = false;
        //                tsbPatientStmtNotes.Visible = false;
        //                tsb_btnNewStatement.Visible = false;
        //                tbVoidStatment.Visible = false;
        //            }
        //            else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Statement))
        //            {
        //                tsbViewHistory.Visible = false;
        //                tsb_ShowDetails.Enabled = true;
        //                //ts_VoidPayment.Enabled = true;
        //                tsbPatientPmnt.Visible = false;
        //                tsbPatientPmnt.Enabled = true;
        //                tsb_ShowDetails.Visible = false;
        //                tsb_ShowZeroBalance.Visible = false;
        //                tsb_HideZeroBalance.Visible = false;
        //                //ts_VoidPayment.Visible = false;
        //                tsb_Modify.Visible = false;
        //                tsb_PatRefund.Visible = false;
        //                tsb_ViewPatRefund.Visible = false;
        //                tsb_ViewStmt.Visible = true;
        //                tsbPatientStmtNotes.Visible = true;
        //                tsb_btnNewStatement.Visible = true;
        //                tbVoidStatment.Visible = true;  

        //                int _IsPatientExcluded =0;
        //                try
        //                {
        //                    this.Fill_PatientStatement(out dsPatFinView);
        //                    dsPatFinView.Tables[0].TableName = "Statement";
        //                    this.c1FlexGrid_Statements.DataMember = "Statement";
        //                    this.c1FlexGrid_Statements.DataSource = dsPatFinView;
        //                    DataTable dt = dsPatFinView.Tables[0];

        //                    c1FlexGrid_Statements.Cols[11].Visible = false;
        //                    c1FlexGrid_Statements.Cols[13].Name = "Note";
        //                    c1FlexGrid_Statements.Cols[13].Caption = "";
        //                    c1FlexGrid_Statements.Cols[13].Width = 18;
        //                    c1FlexGrid_Statements.Cols[13].Visible = true;
        //                    c1FlexGrid_Statements.Cols[13].DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
        //                    c1FlexGrid_Statements.Cols[13].ComboList = "...";
        //                    c1FlexGrid_Statements.Cols[13].AllowEditing = true;
        //                    c1FlexGrid_Statements.Cols[13].ImageAlign = ImageAlignEnum.CenterCenter;

        //                    if (c1FlexGrid_Statements != null && c1FlexGrid_Statements.Cols.Count > 0)
        //                    {
        //                        for (int colInd = 0; colInd < c1FlexGrid_Statements.Cols.Count; colInd++)
        //                        {
        //                            if (c1FlexGrid_Statements.Cols[colInd].Name != "Note")
        //                            {
        //                                c1FlexGrid_Statements.Cols[colInd].AllowEditing = false;
        //                            }
        //                        }
        //                    }
        //                    if (c1FlexGrid_Statements != null && c1FlexGrid_Statements.Rows.Count > 0)
        //                    {
        //                        for (int RowInd = 1; RowInd < c1FlexGrid_Statements.Rows.Count; RowInd++)
        //                        {
        //                            if (Convert.ToString(c1FlexGrid_Statements.Rows[RowInd]["Status"].ToString()) != "Voided")
        //                            {
        //                                c1FlexGrid_Statements.Rows[RowInd].AllowEditing = false;
        //                            }
        //                        }
        //                    }

        //                    _IsPatientExcluded = GetExcludePatientDtl();

        //                    if (_IsPatientExcluded > 0)
        //                    {
        //                        lblPatExcluded.Visible = true;
        //                        //Indicates Account Selected with out any specific patient
        //                        if (_nSelectedPatientId == 0) 
        //                        {
        //                            lblPatExcluded.Text = "Account excluded from statements";
        //                        }
        //                        else
        //                        {
        //                            lblPatExcluded.Text = "Patient excluded from statements";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        lblPatExcluded.Visible = false;
        //                    }
        //                }
        //                catch (Exception ex3)
        //                {
        //                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex3.Message, true);
        //                }


        //            }
        //            else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Insurance_Reserves_and_Refunds))
        //            {
        //                tsbViewHistory.Visible = false;
        //                tsb_ShowDetails.Enabled = true;
        //                //ts_VoidPayment.Enabled = true;
        //                tsb_ShowDetails.Visible = false;
        //                tsb_ShowZeroBalance.Visible = false;
        //                tsb_HideZeroBalance.Visible = false;
        //                tsbPatientPmnt.Visible = false;
        //                tsbPatientPmnt.Enabled = true;
        //                //ts_VoidPayment.Visible = false;
        //                tsb_Modify.Visible = false;
        //                tsb_PatRefund.Visible = false;
        //                tsb_ViewPatRefund.Visible = false;
        //                tsb_ViewStmt.Visible = false;
        //                tsbPatientStmtNotes.Visible = false;
        //                tsb_btnNewStatement.Visible = false;
        //                tbVoidStatment.Visible = false;
        //                FillAssociatedPatInsReserves();
        //                LoadInsuranceRefundLog();
        //            }
        //            else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Summary))
        //            {
        //                tsbViewHistory.Visible = false;
        //                tsb_ShowDetails.Enabled = true;
        //                //ts_VoidPayment.Enabled = true;
        //                tsbPatientPmnt.Visible = false;
        //                tsbPatientPmnt.Enabled = true;
        //                tsb_ShowDetails.Visible = false;
        //                tsb_ShowZeroBalance.Visible = false;
        //                tsb_HideZeroBalance.Visible = false;
        //                //ts_VoidPayment.Visible = false;
        //                tsb_Modify.Visible = false;
        //                tsb_PatRefund.Visible = false;
        //                tsb_ViewPatRefund.Visible = false;
        //                tsb_ViewStmt.Visible = false;
        //                tsbPatientStmtNotes.Visible = false;
        //                tsb_btnNewStatement.Visible = false;
        //                tbVoidStatment.Visible = false;

        //                //Code added by SaiKrishna for Account Feature regarding rowsytle
        //                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle = c1FlexGridSummary.Styles.Add("cs_SummaryRowStyle");
        //                csClaimRowStyle.DataType = typeof(System.String);
        //                csClaimRowStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        //                csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);


        //                if (this.rbtn_Responsibility.Checked)
        //                {
        //                    _AgingSortByInSummary = 'R';
        //                }
        //                else if (this.rbtn_DOS_Summary.Checked)
        //                {
        //                    _AgingSortByInSummary = 'S';
        //                }
        //                try
        //                {
        //                    //Code added by SaiKrishna for account feature.Get the accountpatients based on accountid and Patientid 
        //                    DataTable dtAccPatients = new DataTable();
        //                    oParameters.Add("@nPAccountId", _nPAccountId, ParameterDirection.Input, SqlDbType.BigInt);
        //                    oParameters.Add("@nPatientId", _nSelectedPatientId, ParameterDirection.Input, SqlDbType.BigInt);
        //                    oDB.Connect(false);
        //                    oDB.Retrive("PA_Select_Accounts_Patients", oParameters, out dtAccPatients);
        //                    oDB.Disconnect();
        //                    if (dtAccPatients != null && dtAccPatients.Rows.Count > 0)
        //                    {
        //                        decimal grandTotDecZeroThirty = 0;
        //                        decimal grandTotdecThirtySixty = 0;
        //                        decimal grandTotdecSixtyNinety = 0;
        //                        decimal grandTotdecNinetyHundredTwenty = 0;
        //                        decimal grandTotdecHundredTwentyPlus = 0;
        //                        decimal grandTotdecTotal = 0;
        //                        decimal decTotal = 0;
        //                        for (int i = 0; i < dtAccPatients.Rows.Count; i++)
        //                        {
        //                            string sPatientName = dtAccPatients.Rows[i]["FirstName"].ToString() + ' ' + (dtAccPatients.Rows[i]["MiddleName"].ToString() != "" ? dtAccPatients.Rows[i]["MiddleName"].ToString() + ' ' : "") + dtAccPatients.Rows[i]["LastName"].ToString();
        //                            oParameters.Clear();
        //                            oParameters.Add("@nPatientID", Convert.ToInt64(dtAccPatients.Rows[i]["PatientID"].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
        //                            oParameters.Add("@nPAccountID", _nPAccountId, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oParameters.Add("@nClinicID", this.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oParameters.Add("@nSortBy", this._AgingSortByInSummary, ParameterDirection.Input, SqlDbType.Char);
        //                            oDB.Connect(false);
        //                            oDB.Retrive("PA_Patient_Financial_View_Summary", oParameters, out dsPatFinView);
        //                            oDB.Disconnect();
        //                            dsPatFinView.Tables[0].TableName = "Summary";
        //                            dsPatFinView.Tables["Summary"].Columns.Add("Total", typeof(System.Decimal));
        //                            decimal decZeroThirty = 0;
        //                            decimal decThirtySixty = 0;
        //                            decimal decSixtyNinety = 0;
        //                            decimal decNinetyHundredTwenty = 0;
        //                            decimal decHundredTwentyPlus = 0;
        //                            decTotal = 0;
        //                            if (i == 0)
        //                            {
        //                                dtAccPatSummary = dsPatFinView.Tables["Summary"].Clone();
        //                                dtAccPatSummary.Columns.Add("PatName", typeof(System.String));
        //                            }

        //                            DataRow dr = dtAccPatSummary.NewRow();
        //                            dr["PatName"] = sPatientName;
        //                            dtAccPatSummary.Rows.Add(dr);

        //                            if (dsPatFinView.Tables["Summary"].Rows.Count > 0)
        //                            {

        //                                for (cntr = 0; cntr <= (dsPatFinView.Tables["Summary"].Rows.Count - 1); cntr++)
        //                                {

        //                                    dsPatFinView.Tables["Summary"].Rows[cntr]["Total"] = Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["0-30"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["0-30"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["31-60"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["31-60"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["61-90"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["61-90"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["91-120"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["91-120"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["120+"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["120+"]);
        //                                    decZeroThirty += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["0-30"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["0-30"]);
        //                                    decThirtySixty += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["31-60"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["31-60"]);
        //                                    decSixtyNinety += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["61-90"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["61-90"]);
        //                                    decNinetyHundredTwenty += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["91-120"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["91-120"]);
        //                                    decHundredTwentyPlus += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["120+"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["120+"]);
        //                                    decTotal += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["Total"]);
        //                                    dtAccPatSummary.ImportRow(dsPatFinView.Tables["Summary"].Rows[cntr]);
        //                                }

        //                                dsPatFinView.Tables["Summary"].Rows.Add();
        //                                dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["ResponsibleParty"] = "Total :";
        //                                dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["0-30"] = decZeroThirty;
        //                                dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["31-60"] = decThirtySixty;
        //                                dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["61-90"] = decSixtyNinety;
        //                                dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["91-120"] = decNinetyHundredTwenty;
        //                                dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["120+"] = decHundredTwentyPlus;
        //                                dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["Total"] = decTotal;
        //                                dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["dtLastBilled"] = DBNull.Value;
        //                                dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["dtLastRemitteded"] = DBNull.Value;
        //                                dtAccPatSummary.ImportRow(dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]);


        //                                //calulation for grandtotal
        //                                grandTotDecZeroThirty += Convert.ToDecimal(decZeroThirty);
        //                                grandTotdecThirtySixty += Convert.ToDecimal(decThirtySixty);
        //                                grandTotdecSixtyNinety += Convert.ToDecimal(decSixtyNinety);
        //                                grandTotdecNinetyHundredTwenty += Convert.ToDecimal(decNinetyHundredTwenty);
        //                                grandTotdecHundredTwentyPlus += Convert.ToDecimal(decHundredTwentyPlus);
        //                                grandTotdecTotal += decTotal;
        //                            }


        //                        }
        //                        if (grandTotdecTotal != decTotal)
        //                        {
        //                            dtAccPatSummary.Rows.Add();
        //                            dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["ResponsibleParty"] = "Grand Total :";
        //                            dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["0-30"] = grandTotDecZeroThirty;
        //                            dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["31-60"] = grandTotdecThirtySixty;
        //                            dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["61-90"] = grandTotdecSixtyNinety;
        //                            dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["91-120"] = grandTotdecNinetyHundredTwenty;
        //                            dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["120+"] = grandTotdecHundredTwentyPlus;
        //                            dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["Total"] = grandTotdecTotal;
        //                            dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["dtLastBilled"] = DBNull.Value;
        //                            dtAccPatSummary.Rows[dtAccPatSummary.Rows.Count - 1]["dtLastRemitteded"] = DBNull.Value;
        //                        }
        //                    }

        //                    //if (dsPatFinView.Tables["Summary"].Rows.Count > 0)
        //                    //{
        //                    //    for (int cntrRow = 0; cntrRow <= (dsPatFinView.Tables["Summary"].Rows.Count - 1); cntrRow++)
        //                    //    {
        //                    //        dsPatFinView.Tables["Summary"].Rows[cntrRow]["Total"] = Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntrRow]["0-30"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntrRow]["0-30"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntrRow]["31-60"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntrRow]["31-60"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntrRow]["61-90"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntrRow]["61-90"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntrRow]["91-120"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntrRow]["91-120"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntrRow]["120+"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntrRow]["120+"]);
        //                    //    }
        //                    //}

        //                    dsAccPatSummary.Tables.Add(dtAccPatSummary);
        //                    this.c1FlexGridSummary.DataMember = "Summary";
        //                    this.c1FlexGridSummary.DataSource = dsAccPatSummary;

        //                    if (c1FlexGridSummary.Rows.Count > 1)
        //                    {
        //                        for (int intCnt = 0; intCnt < c1FlexGridSummary.Rows.Count - 1; intCnt++)
        //                        {
        //                            if (c1FlexGridSummary.Rows[intCnt]["ResponsibleParty"].ToString() == "")
        //                            {
        //                                c1FlexGridSummary.Rows[intCnt].Style = c1FlexGridSummary.Styles["cs_SummaryRowStyle"];
        //                            }
        //                            if (c1FlexGridSummary.Rows[intCnt]["ResponsibleParty"].ToString() == "Total :")
        //                            {
        //                                setGridStyle(c1FlexGridSummary, intCnt, 1, 8);
        //                                c1FlexGridSummary.Row = intCnt;
        //                            }
        //                        }

        //                        setGridStyle(c1FlexGridSummary, c1FlexGridSummary.Rows.Count - 1, 1, 8);
        //                        c1FlexGridSummary.Row = c1FlexGridSummary.Rows.Count - 1;


        //                    }



        //                }
        //                catch (Exception ex3)
        //                {
        //                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex3.Message, true);
        //                }
        //            }
        //            else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Report))
        //            {
        //                tsbViewHistory.Visible = false;
        //                tsb_ShowDetails.Enabled = true;
        //                //ts_VoidPayment.Enabled = true;
        //                tsbPatientPmnt.Visible = false;
        //                tsbPatientPmnt.Enabled = true;
        //                tsb_ShowDetails.Visible = false;
        //                tsb_ShowZeroBalance.Visible = false;
        //                tsb_HideZeroBalance.Visible = false;
        //                //ts_VoidPayment.Visible = false;
        //                tsb_Modify.Visible = false;
        //                tsb_PatRefund.Visible = false;
        //                tsb_ViewPatRefund.Visible = false;
        //                tsb_ViewStmt.Visible = false;
        //                tsbPatientStmtNotes.Visible = false;
        //                tsb_btnNewStatement.Visible = false;
        //                tbVoidStatment.Visible = false;

        //                //RETRIVING REPORT SERVER NAME
        //                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings( gloPMGlobal.DatabaseConnectionString);
        //                object oValue = new object();
        //                oSetting.GetSetting("ReportServer", out oValue);
        //                if (oValue != null)
        //                {
        //                    strReportServer = oValue.ToString();
        //                    oValue = null;
        //                }

        //                //RETRIVING REPORT FOLDER NAME
        //                oSetting.GetSetting("ReportFolder", out oValue);
        //                if (oValue != null)
        //                {
        //                    strReportFolder = oValue.ToString();
        //                    oValue = null;
        //                }

        //                //RETRIVING VIRTUAL DIRECTORY NAME
        //                oSetting.GetSetting("ReportVirtualDirectory", out oValue);
        //                if (oValue != null)
        //                {
        //                    strVirtualDir = oValue.ToString();
        //                    oValue = null;

        //                }

        //                string reportParam = "&suser=" + gloPMGlobal.UserName + "&Practice=" + getClinicName();// +"&Conn=" +  gloPMGlobal.DatabaseConnectionString;
        //                //this.Text = _reportTitle;

        //                List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();





        //                 SSRSReportURL = new Uri("http://" + strReportServer + "/" + strVirtualDir);

        //                PAHreportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
        //                PAHreportViewer.ServerReport.ReportServerUrl = SSRSReportURL;



        //                    //axWebBrowser1.Navigate("http://" + strReportServer + "/" + strVirtualDir + "?/" + strReportFolder + "/" + _reportName + reportParam + "&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=true");
        //                PAHreportViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + "rptPatientAccountHistory";// +reportParam;
        //                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", gloPMGlobal.UserName, false));
        //                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
        //                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("nPatientID", _nPatientID.ToString() , false));
        //                //paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Conn",  gloPMGlobal.DatabaseConnectionString, false));
        //                this.PAHreportViewer.ServerReport.SetParameters(paramList);
        //                this.PAHreportViewer.RefreshReport();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //            if (oDB != null)
        //                oDB.Dispose();
        //            if (oParameters != null)
        //                oParameters.Dispose();
        //        }
        //    }
        //    finally
        //    {
        //        if (oDB != null)
        //            oDB.Dispose();
        //        if (oParameters != null)
        //            oParameters.Dispose();
        //    }
        //}
        #endregion

        //Added By Shweta
        private void tbPatientFinancial_SelectedIndexChanged(object sender, EventArgs e)
       {
            
            DataTable dtAccPatSummary = new DataTable("Summary");
            DataSet dsAccPatSummary = new DataSet();
            tsp_AccNotes.Visible = false;
            tsbPaymentPlan.Visible = false;
            tsp_ViewLog.Visible = false;
            tsp_AccFollowUp.Visible = false;
            chkIncludeVoids.Visible = false;
            oPatientControl.DisableChkAllAcctPat = false;

            try
            {
                switch (this.tbPatientFinancial.SelectedTab.Name)
                {
                    case "tbPgSummary": //Summary
                        tsb_btnCollAgencyRefund.Visible = false;
                        tsbViewHistory.Visible = false;
                        tsb_ShowDetails.Enabled = true;
                        tsbPatientPmnt.Visible = false;
                        tsbPatientPmnt.Enabled = true;
                        tsb_ShowDetails.Visible = false;
                        tsb_ShowZeroBalance.Visible = false;
                        tsb_HideZeroBalance.Visible = false;
                        tsb_Modify.Visible = false;
                        tsb_PatRefund.Visible = false;
                        tsb_ViewPatRefund.Visible = false;
                        tsb_ViewStmt.Visible = false;
                        tsbPatientStmtNotes.Visible = false;
                        tsb_btnNewStatement.Visible = false;
                        tsb_btnGWStatement.Visible = false;
                        tbVoidStatment.Visible = false;
                        tsb_GlobalPeriods.Visible = false;
                        tsb_ModifyGlobalPeriods.Visible = false;
                        tsb_DeleteGlobalPeriod.Visible = false;
                        tsb_StatementCount.Visible = false;
                        tsb_ViewExamNote.Visible = false;
                        tsb_btnTransferResponsibility.Visible = false;
                        ts_btnPrint.Visible = false;
                        tsb_Pat_OA_AddPaymentPlan.Visible = false;
                        tsb_Pat_OA_EditPaymentPlan.Visible = false;
                        tsb_Pat_OA_OneTimePayment.Visible = false;
                        tsb_Pat_OA_Subscriptions.Visible = false;
                        tsb_Pat_OA_Transactions.Visible = false;
                        tsb_Pat_OA_AccountOnFile.Visible = false;
                        //if (tbPgSummary.Tag.ToString() != "isSummaryLoaded")
                        //{
                        //    FillSummary();
                        //}

                        if (Convert.ToString(tbPgSummary.Tag) != "isSummaryLoaded")
                        {
                            oPatientControl.DisableInputControl = true;
                             pnlProgressIndicationSummary.BringToFront();

                            if (!Summaryworker.IsBusy)
                            {
                                tbPgSummary.Tag = "isSummaryLoaded";
                                Summaryworker.RunWorkerAsync();
                            }
                            else
                            {
                                Summaryworker.CancelAsync();
                            }
                        }
                        break;

                    case "tbpgCharge_Claims": //Claim_Details
                        this.Cursor = Cursors.WaitCursor;
                        tsb_btnCollAgencyRefund.Visible = false;
                        tsb_ShowDetails.Visible = false;
                        tsb_ShowDetails.Enabled = false;
                        tsbPatientPmnt.Visible = false;
                        tsbPatientPmnt.Enabled = false;
                        tsb_PatRefund.Visible = false;
                        tsb_ViewPatRefund.Visible = false;
                        tsb_ViewStmt.Visible = false;
                        tsbPatientStmtNotes.Visible = false;
                        tsb_btnNewStatement.Visible = false;
                        tsb_btnGWStatement.Visible = false;
                        tbVoidStatment.Visible = false;
                        //if (_blnShowZeroBal == true)
                        //{
                        tsb_HideZeroBalance.Visible = false;
                        tsb_ShowZeroBalance.Visible = true;                        
                        //}
                        //else
                        //{
                        //    tsb_HideZeroBalance.Visible = true;
                        //    tsb_ShowZeroBalance.Visible = false;
                        //}
                        //ts_VoidPayment.Visible = false;
                        tsb_GlobalPeriods.Visible = false;
                        tsb_ModifyGlobalPeriods.Visible = false;
                        tsb_DeleteGlobalPeriod.Visible = false;
                        tsb_StatementCount.Visible = false;
                        tsb_Modify.Visible = true;
                        tsb_ViewExamNote.Visible = true;
                        tsb_btnTransferResponsibility.Visible = true;
                        ts_btnPrint.Visible = false;
                        tsb_Pat_OA_AddPaymentPlan.Visible = false;
                        tsb_Pat_OA_EditPaymentPlan.Visible = false;
                        tsb_Pat_OA_OneTimePayment.Visible = false;
                        tsb_Pat_OA_Subscriptions.Visible = false;
                        tsb_Pat_OA_Transactions.Visible = false;
                        tsb_Pat_OA_AccountOnFile.Visible = false;

                        if (Convert.ToString(tsbViewHistory.Tag) == "Show")
                        {
                            tsbViewHistory.Visible = true;
                        }
                        else if (Convert.ToString(tsbViewHistory.Tag) == "Hide")
                        {
                            tsbViewHistory.Visible = false;
                        }
                        if (tbpgCharge_Claims.Tag.ToString() != "isClaimNChargesLoaded")
                        {    
                            FillClaim_Charges();
                        }
                        
                       // showHideZeroBalances();
                        this.Cursor = Cursors.Default;
                        break;
                    case "tbpgPatPmt"://Patient_Payment_and_Reserves
                        gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                        isBadDebtPatient = oSecurity.isBadDebtPatient(_nPatientID, true);
                        oSecurity.Dispose();
                        oSecurity = null;
                        tsbViewHistory.Visible = false;
                        tsb_ShowDetails.Visible = true;
                        tsb_ShowDetails.Enabled = false;
                        tsbPatientPmnt.Visible = true;
                        tsbPatientPmnt.Enabled = true;
                        tsb_Modify.Visible = false;
                        tsb_ShowZeroBalance.Visible = false;
                        tsb_HideZeroBalance.Visible = false;
                        tsb_PatRefund.Visible = true;
                        tsb_PatRefund.Enabled = false;
                        tsb_ViewPatRefund.Visible = true;
                        tsb_ViewPatRefund.Enabled = false;
                        tsb_ViewStmt.Visible = false;
                        tsbPatientStmtNotes.Visible = false;
                        tsb_btnNewStatement.Visible = false;
                        tsb_btnGWStatement.Visible = false;
                        tbVoidStatment.Visible = false;
                        tsb_GlobalPeriods.Visible = false;
                        tsb_ModifyGlobalPeriods.Visible = false;
                        tsb_DeleteGlobalPeriod.Visible = false;
                        tsb_StatementCount.Visible = false;
                        tsb_ViewExamNote.Visible = false;
                        tsb_btnTransferResponsibility.Visible = false;
                        ts_btnPrint.Visible = false;
                        tsb_Pat_OA_AddPaymentPlan.Visible = false;
                        tsb_Pat_OA_EditPaymentPlan.Visible = false;
                        tsb_Pat_OA_OneTimePayment.Visible = false;
                        tsb_Pat_OA_Subscriptions.Visible = false;
                        tsb_Pat_OA_Transactions.Visible = false;
                        tsb_Pat_OA_AccountOnFile.Visible = false;
                        if (isBadDebtPatient)
                        {
                            tsb_btnCollAgencyRefund.Visible = true;
                        }
                        else
                        {
                            tsb_btnCollAgencyRefund.Visible = false;
                        }
                        if (tbpgPatPmt.Tag.ToString() != "isPatPmtLoaded")
                        {
                            FillPatientPayment();
                            FillPatientRefunds();
                            FillPatientReserves();
                        }
                       
                        break;
                    case "tbpgReport"://Report
                        tsb_btnCollAgencyRefund.Visible = false;
                        tsbViewHistory.Visible = false;
                        tsb_ShowDetails.Enabled = true;
                        tsbPatientPmnt.Visible = false;
                        tsbPatientPmnt.Enabled = true;
                        tsb_ShowDetails.Visible = false;
                        tsb_ShowZeroBalance.Visible = false;
                        tsb_HideZeroBalance.Visible = false;
                        tsb_Modify.Visible = false;
                        tsb_PatRefund.Visible = false;
                        tsb_ViewPatRefund.Visible = false;
                        tsb_ViewStmt.Visible = false;
                        tsbPatientStmtNotes.Visible = false;
                        tsb_btnNewStatement.Visible = false;
                        tsb_btnGWStatement.Visible = false;
                        tbVoidStatment.Visible = false;
                        tsb_GlobalPeriods.Visible = false;
                        tsb_ModifyGlobalPeriods.Visible = false;
                        tsb_DeleteGlobalPeriod.Visible = false;
                        tsb_StatementCount.Visible = false;
                        tsb_ViewExamNote.Visible = false;
                        tsb_btnTransferResponsibility.Visible = false;
                        ts_btnPrint.Visible = true;
                        tsb_Pat_OA_AddPaymentPlan.Visible = false;
                        tsb_Pat_OA_EditPaymentPlan.Visible = false;
                        tsb_Pat_OA_OneTimePayment.Visible = false;
                        tsb_Pat_OA_Subscriptions.Visible = false;
                        tsb_Pat_OA_Transactions.Visible = false;
                        tsb_Pat_OA_AccountOnFile.Visible = false;
                        if (tbpgReport.Tag.ToString() != "isReportLoaded" && rbClaim.Checked == true)
                        {
                            GenerateReport();
                        }
                        else if (tbpgReport.Tag.ToString() != "isPaymentReportLoaded" && rbPayment.Checked == true)
                        {
                             GenerateReport();
                        }
                        break;
                    case "tbpgStatements"://Statement
                        tsb_btnCollAgencyRefund.Visible = false;
                        tsbViewHistory.Visible = false;
                        tsb_ShowDetails.Enabled = true;
                        tsbPatientPmnt.Visible = false;
                        tsbPatientPmnt.Enabled = true;
                        tsb_ShowDetails.Visible = false;
                        tsb_ShowZeroBalance.Visible = false;
                        tsb_HideZeroBalance.Visible = false;
                        tsb_Modify.Visible = false;
                        tsb_PatRefund.Visible = false;
                        tsb_ViewPatRefund.Visible = false;
                        tsb_ViewStmt.Visible = true;
                        tsbPatientStmtNotes.Visible = true;
                        tsb_btnNewStatement.Visible = true;
                        tsb_btnGWStatement.Visible = true;
                        tbVoidStatment.Visible = true;
                        tsb_GlobalPeriods.Visible = false;
                        tsb_ModifyGlobalPeriods.Visible = false;
                        tsb_DeleteGlobalPeriod.Visible = false;
                        tsb_StatementCount.Visible = true;
                        tsb_ViewExamNote.Visible = false;
                        tsb_btnTransferResponsibility.Visible = false;
                        ts_btnPrint.Visible = false;
                        tsb_Pat_OA_AddPaymentPlan.Visible = false;
                        tsb_Pat_OA_EditPaymentPlan.Visible = false;
                        tsb_Pat_OA_OneTimePayment.Visible = false;
                        tsb_Pat_OA_Subscriptions.Visible = false;
                        tsb_Pat_OA_Transactions.Visible = false;
                        tsb_Pat_OA_AccountOnFile.Visible = false;
                        if (tbpgStatements.Tag.ToString() != "isStatementLoaded")
                        {
                            FillStatement();
                        }
                        break;
                    case "tbpgInsResAndRefund"://Insurance_Reserves_and_Refunds
                        tsb_btnCollAgencyRefund.Visible = false;
                        tsbViewHistory.Visible = false;
                        tsb_ShowDetails.Enabled = true;
                        tsb_ShowDetails.Visible = false;
                        tsb_ShowZeroBalance.Visible = false;
                        tsb_HideZeroBalance.Visible = false;
                        tsbPatientPmnt.Visible = false;
                        tsbPatientPmnt.Enabled = true;
                        tsb_Modify.Visible = false;
                        tsb_PatRefund.Visible = false;
                        tsb_ViewPatRefund.Visible = false;
                        tsb_ViewStmt.Visible = false;
                        tsbPatientStmtNotes.Visible = false;
                        tsb_btnNewStatement.Visible = false;
                        tsb_btnGWStatement.Visible = false;
                        tbVoidStatment.Visible = false;
                        tsb_GlobalPeriods.Visible = false;
                        tsb_ModifyGlobalPeriods.Visible = false;
                        tsb_DeleteGlobalPeriod.Visible = false;
                        tsb_StatementCount.Visible = false;
                        tsb_ViewExamNote.Visible = false;
                        tsb_btnTransferResponsibility.Visible = false;
                        ts_btnPrint.Visible = false;
                        tsb_Pat_OA_AddPaymentPlan.Visible = false;
                        tsb_Pat_OA_EditPaymentPlan.Visible = false;
                        tsb_Pat_OA_OneTimePayment.Visible = false;
                        tsb_Pat_OA_Subscriptions.Visible = false;
                        tsb_Pat_OA_Transactions.Visible = false;
                        tsb_Pat_OA_AccountOnFile.Visible = false;
                        if (tbpgInsResAndRefund.Tag.ToString() != "isInsResNRefLoaded")
                        {
                            FillInsuranceReserves();
                            LoadInsuranceRefundLog();
                        }
                        break;
                    case "tbpgChronology"://Chronology 
                        tsb_btnCollAgencyRefund.Visible = false;
                        tsbViewHistory.Visible = false;
                        tsb_ShowDetails.Enabled = true;
                        tsb_ShowDetails.Visible = false;
                        tsbPatientPmnt.Visible = false;
                        tsbPatientPmnt.Enabled = true;
                        tsb_ShowZeroBalance.Visible = false;
                        tsb_HideZeroBalance.Visible = false;
                        tsb_Modify.Visible = false;
                        tsb_PatRefund.Visible = false;
                        tsb_ViewPatRefund.Visible = false;
                        tsb_ViewStmt.Visible = false;
                        tsbPatientStmtNotes.Visible = false;
                        tsb_btnNewStatement.Visible = false;
                        tsb_btnGWStatement.Visible = false;
                        tbVoidStatment.Visible = false;
                        tsb_GlobalPeriods.Visible = false;
                        tsb_ModifyGlobalPeriods.Visible = false;
                        tsb_DeleteGlobalPeriod.Visible = false;
                        tsb_StatementCount.Visible = false;
                        tsb_ViewExamNote.Visible = false;
                        tsb_btnTransferResponsibility.Visible = false;
                        ts_btnPrint.Visible = false;
                        tsb_Pat_OA_AddPaymentPlan.Visible = false;
                        tsb_Pat_OA_EditPaymentPlan.Visible = false;
                        tsb_Pat_OA_OneTimePayment.Visible = false;
                        tsb_Pat_OA_Subscriptions.Visible = false;
                        tsb_Pat_OA_Transactions.Visible = false;
                        tsb_Pat_OA_AccountOnFile.Visible = false;
                        break;
                    case "tbpgInsRemit"://Insurance_Remittance
                        tsb_btnCollAgencyRefund.Visible = false;
                        tsbViewHistory.Visible = false;
                        tsb_ShowDetails.Enabled = true;
                        tsb_ShowDetails.Visible = false;
                        tsb_ShowZeroBalance.Visible = false;
                        tsb_HideZeroBalance.Visible = false;
                        tsbPatientPmnt.Visible = false;
                        tsbPatientPmnt.Enabled = true;
                        tsb_Modify.Visible = false;
                        tsb_PatRefund.Visible = false;
                        tsb_ViewPatRefund.Visible = false;
                        tsb_ViewStmt.Visible = false;
                        tsbPatientStmtNotes.Visible = false;
                        tsb_btnNewStatement.Visible = false;
                        tsb_btnGWStatement.Visible = false;
                        tbVoidStatment.Visible = false;
                        tsb_GlobalPeriods.Visible = false;
                        tsb_ModifyGlobalPeriods.Visible = false;
                        tsb_DeleteGlobalPeriod.Visible = false;
                        tsb_StatementCount.Visible = false;
                        tsb_ViewExamNote.Visible = false;
                        tsb_btnTransferResponsibility.Visible = false;
                        ts_btnPrint.Visible = false;
                        tsb_Pat_OA_AddPaymentPlan.Visible = false;
                        tsb_Pat_OA_EditPaymentPlan.Visible = false;
                        tsb_Pat_OA_OneTimePayment.Visible = false;
                        tsb_Pat_OA_Subscriptions.Visible = false;
                        tsb_Pat_OA_Transactions.Visible = false;
                        tsb_Pat_OA_AccountOnFile.Visible = false;
                        break;
                    case "tbpgGlobalPeriods"://Global Periods
                        tsb_btnCollAgencyRefund.Visible = false;
                        tsb_GlobalPeriods.Visible = true;
                        tsb_ModifyGlobalPeriods.Visible = true;
                        tsb_DeleteGlobalPeriod.Visible = true;
                        tsbViewHistory.Visible = false;
                        tsb_ShowDetails.Visible = false;
                        tsb_ShowZeroBalance.Visible = false;
                        tsb_HideZeroBalance.Visible = false;
                        tsbPatientPmnt.Visible = false;
                        tsb_Modify.Visible = false;
                        tsb_PatRefund.Visible = false;
                        tsb_ViewPatRefund.Visible = false;
                        tsb_ViewStmt.Visible = false;
                        tsbPatientStmtNotes.Visible = false;
                        tsb_btnNewStatement.Visible = false;
                        tsb_btnGWStatement.Visible = false;
                        tbVoidStatment.Visible = false;
                        tsb_StatementCount.Visible = false;
                        tsb_ViewExamNote.Visible = false;
                        tsb_btnTransferResponsibility.Visible = false;
                        ts_btnPrint.Visible = false;
                        tsb_Pat_OA_AddPaymentPlan.Visible = false;
                        tsb_Pat_OA_EditPaymentPlan.Visible = false;
                        tsb_Pat_OA_OneTimePayment.Visible = false;
                        tsb_Pat_OA_Subscriptions.Visible = false;
                        tsb_Pat_OA_Transactions.Visible = false;
                        tsb_Pat_OA_AccountOnFile.Visible = false;
                        if (tbpgGlobalPeriods.Tag.ToString() != "isGlobalPeriodsLoaded")
                        {
                            FillGlobalPeriods();
                        }
                        break;
                    case "tbpgAccountLog"://Account Log
                        if (_nPatientID == 0)
                        {
                            tsp_AccFollowUp.Enabled = false;
                            tsbPaymentPlan.Enabled = false;
                        }
                        else
                        {
                            tsp_AccFollowUp.Enabled = true;
                            tsbPaymentPlan.Enabled = true;
                        }
                        tsb_btnCollAgencyRefund.Visible = false;
                        tsp_AccFollowUp.Visible = true;;
                        tsbPaymentPlan.Visible = true;;
                        tsp_ViewLog.Visible = true;
                        tsp_AccNotes.Visible = true;
                       
                        tsbViewHistory.Visible = false;
                        tsb_ShowDetails.Enabled = false;
                        tsbPatientPmnt.Visible = false;
                        tsbPatientPmnt.Enabled = false;
                        tsb_ShowDetails.Visible = false;
                        tsb_ShowZeroBalance.Visible = false;
                        tsb_HideZeroBalance.Visible = false;
                        tsb_Modify.Visible = false;
                        tsb_PatRefund.Visible = false;
                        tsb_ViewPatRefund.Visible = false;
                        tsb_ViewStmt.Visible = false;
                        tsbPatientStmtNotes.Visible = false;
                        tsb_btnNewStatement.Visible = false;
                        tsb_btnGWStatement.Visible = false;
                        tbVoidStatment.Visible = false;
                        tsb_GlobalPeriods.Visible = false;
                        tsb_ModifyGlobalPeriods.Visible = false;
                        tsb_DeleteGlobalPeriod.Visible = false;
                        tsb_StatementCount.Visible = false;
                        tsb_ViewExamNote.Visible = false;
                        tsb_btnTransferResponsibility.Visible = false;
                        oPatientControl.IsAllAccPatSelected = true;
                        oPatientControl.DisableChkAllAcctPat = true;
                        ts_btnPrint.Visible = false;
                        tsb_Pat_OA_AddPaymentPlan.Visible = false;
                        tsb_Pat_OA_EditPaymentPlan.Visible = false;
                        tsb_Pat_OA_OneTimePayment.Visible = false;
                        tsb_Pat_OA_Subscriptions.Visible = false;
                        tsb_Pat_OA_Transactions.Visible = false;
                        tsb_Pat_OA_AccountOnFile.Visible = false;

                        if (Convert.ToString(tbpgAccountLog.Tag) != "isAccountLogLoaded")
                        {
                            oPatientControl.DisableInputControl = true;
                            pnlProgressIndication.BringToFront();
                           
                            if (!worker.IsBusy)
                            {
                                tbpgAccountLog.Tag = "isAccountLogLoaded";
                                worker.RunWorkerAsync();
                            }
                            else
                            {
                                worker.CancelAsync();
                            }
                        }
                        
                       
                        oPatientControl.RefreshBalances();
                        break;
                    case "tbpgPatOnlineActivity"://Global Periods
                        tsb_btnCollAgencyRefund.Visible = false;
                        tsb_GlobalPeriods.Visible = false;
                        tsb_ModifyGlobalPeriods.Visible = false;
                        tsb_DeleteGlobalPeriod.Visible = false;
                        tsbViewHistory.Visible = false;
                        tsb_ShowDetails.Visible = false;
                        tsb_ShowZeroBalance.Visible = false;
                        tsb_HideZeroBalance.Visible = false;
                        tsbPatientPmnt.Visible = false;
                        tsb_Modify.Visible = false;
                        tsb_PatRefund.Visible = false;
                        tsb_ViewPatRefund.Visible = false;
                        tsb_ViewStmt.Visible = false;
                        tsbPatientStmtNotes.Visible = false;
                        tsb_btnNewStatement.Visible = false;
                        tsb_btnGWStatement.Visible = false;
                        tbVoidStatment.Visible = false;
                        tsb_StatementCount.Visible = false;
                        tsb_ViewExamNote.Visible = false;
                        tsb_btnTransferResponsibility.Visible = false;
                        ts_btnPrint.Visible = false;
                        tsb_Pat_OA_AddPaymentPlan.Visible = false;
                        tsb_Pat_OA_EditPaymentPlan.Visible = true;
                        tsb_Pat_OA_OneTimePayment.Visible = false;
                        tsb_Pat_OA_Subscriptions.Visible = false;
                        tsb_Pat_OA_Transactions.Visible = true;
                        tsb_Pat_OA_AccountOnFile.Visible = false;
                        //if (tbpgGlobalPeriods.Tag.ToString() != "isGlobalPeriodsLoaded")
                        //{
                        //    FillGlobalPeriods();
                        //}
                        FillOnlineActvity();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                RemoveTabPageTag();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            

        }

        
        void Summaryworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //filter.Filters.Clear();
                //filter.Notes = false;
                this.c1FlexGridSummary.DataMember = "Summary";
                this.c1FlexGridSummary.DataSource = e.Result;

                c1FlexGridSummary.Cols["dtLastRemitteded"].Format = "MM/dd/yyyy";

                //Code added by SaiKrishna for Account Feature regarding rowsytle
                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle;// = c1FlexGridSummary.Styles.Add("cs_SummaryRowStyle");
                try
                {
                    if (c1FlexGridSummary.Styles.Contains("cs_SummaryRowStyle"))
                    {
                        csClaimRowStyle = c1FlexGridSummary.Styles["cs_SummaryRowStyle"];
                    }
                    else
                    {
                        csClaimRowStyle = c1FlexGridSummary.Styles.Add("cs_SummaryRowStyle");
                        csClaimRowStyle.DataType = typeof(System.String);
                        csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }

                }
                catch
                {
                    csClaimRowStyle = c1FlexGridSummary.Styles.Add("cs_SummaryRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                }



                if (c1FlexGridSummary.Rows.Count > 1)
                {
                    for (int intCnt = 0; intCnt < c1FlexGridSummary.Rows.Count - 1; intCnt++)
                    {
                        if (c1FlexGridSummary.Rows[intCnt]["ResponsibleParty"].ToString() == "")
                        {
                            c1FlexGridSummary.Rows[intCnt].Style = c1FlexGridSummary.Styles["cs_SummaryRowStyle"];
                        }
                        if (c1FlexGridSummary.Rows[intCnt]["ResponsibleParty"].ToString() == "Total :")
                        {
                            setGridStyle(c1FlexGridSummary, intCnt, 1, 8);
                            c1FlexGridSummary.Row = intCnt;
                        }
                    }

                    setGridStyle(c1FlexGridSummary, c1FlexGridSummary.Rows.Count - 1, 1, 8);
                    c1FlexGridSummary.Row = c1FlexGridSummary.Rows.Count - 1;
                    tbPgSummary.Tag = "isSummaryLoaded";
                }

                oPatientControl.DisableInputControl = false;
                pnlProgressIndicationSummary.SendToBack();

                if (e.Result == null)
                {
                    RemoveTabPageTag();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                filter.Filters.Clear();
                filter.Notes = false;
                //Added code to avoid the Exception Object reference not set to an instance of an object 
                //if the existing form is closed before the c1PALogView is refreshed.
                if (c1PALogView.IsHandleCreated)
                {
                    if (filter.Filters.Count == 0)
                    {
                        c1PALogView.SetCellStyle(0, (int)GridColumnAccountLog.Type, "HeaderFilterIcon");
                    }

                    c1PALogView.Cols[(int)GridColumnAccountLog.Patient].Visible = false;

                    c1PALogView.AutoResize = false;
                    c1PALogView.Styles.Normal.WordWrap = true;
                    c1PALogView.DataSource = e.Result;


                    for (int iRow = 1; iRow <= c1PALogView.Rows.Count - 1; iRow++)
                    {
                        c1PALogView.AutoSizeRow(iRow);
                    }

                    if (oPatientControl.IsPatientAccFearureEnabled)
                    {
                        c1PALogView.Cols[(int)GridColumnAccountLog.Patient].Visible = true;
                    }
                    else
                    {
                        c1PALogView.Cols[(int)GridColumnAccountLog.Patient].Visible = false;
                    }

                    oPatientControl.DisableInputControl = false;
                    pnlProgressIndication.SendToBack();

                    if (e.Result == null)
                    {
                        RemoveTabPageTag();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                gloPatientFinancialViewV2 objPatFinacialView = null;
                if (oPatientControl.IsAllAccPatSelected)
                {
                    objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
                    if (chkDate.Checked == true)
                    {
                        _dtPALog = objPatFinacialView.GetAccountLog(oPatientControl.PAccountID,dtStartDate.Text,dtEndDate.Text);
                    }
                    else
                    {
                        _dtPALog = objPatFinacialView.GetAccountLog(oPatientControl.PAccountID, null, null);
                    }
                }
                else
                {
                    objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
                    _dtPALog = objPatFinacialView.GetPatientAccountLog(oPatientControl.PatientID, oPatientControl.PAccountID);
                }

                if (_dtPALog != null)
                {
                    var PALogresult = from dt in _dtPALog.AsEnumerable()
                                      select new
                                      {
                                          Closedate = dt.Field<DateTime?>("CloseDate"),
                                          Type = dt.Field<String>("Type"),
                                          Description = Convert.ToString((dt.Field<DateTime?>("DOS").HasValue ? dt.Field<DateTime>("DOS").ToString("MM/dd/yyyy") : "") + " "
                                                        + dt.Field<String>("ClaimNumber") + " "
                                                        + dt.Field<String>("CPTCode") + " "
                                                        + dt.Field<String>("Mod1Code") + " "
                                                        + dt.Field<String>("DX1Code") + Environment.NewLine
                                                        + dt.Field<String>("Description")).Replace("\\\\n", Environment.NewLine).Trim(),
                                          Delta = dt.Field<Decimal?>("Delta"),
                                          Balance = dt.Field<Decimal?>("Balance"),
                                          PatDue = dt.Field<Decimal?>("PatDue"),
                                          UserName = dt.Field<String>("UserName"),
                                          CreateDateTime = dt.Field<DateTime?>("CreateDateTime"),
                                          MstTransactionID = dt.Field<Int64?>("TransactionID"),
                                          TransactionID = dt.Field<Int64?>("ClaimTransactionID"),
                                          TransactionDetailId = dt.Field<Int64?>("TransactionDetailId"),
                                          CreditId = dt.Field<Int64?>("CreditId"),
                                          DebitId = dt.Field<Int64?>("DebitId"),
                                          NoteID = dt.Field<Int64?>("NoteID"),
                                          EOBID = dt.Field<Int64?>("EOBID"),
                                          RefundID = dt.Field<Int64?>("RefundId"),
                                          PatientID = dt.Field<Int64?>("PatientID"),
                                          Patient = dt.Field<String>("PatientName")
                                      };

                    e.Result = LINQToDataTable(PALogresult);
                }
            }
            catch (Exception ex)
            {
                e.Result = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        void Summaryworker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                gloPatientFinancialViewV2 objPatFinacialView = null;
                if (oPatientControl.IsAllAccPatSelected)
                {
                    objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
                }
                else
                {
                    objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);
                }

                DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
                DBParameters oParameters = new DBParameters();
                DataSet dsSummary = new DataSet();
                try
                {
                    if (this.rbtn_Responsibility.Checked)
                    {
                        _AgingSortByInSummary = 'R';
                    }
                    else if (this.rbtn_DOS_Summary.Checked)
                    {
                        _AgingSortByInSummary = 'S';
                    }

                    objPatFinacialView.GetSummary(this._AgingSortByInSummary, out dsSummary);

                    e.Result = dsSummary;

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                    tbPgSummary.Tag = "";
                }
                finally
                {

                    if (objPatFinacialView != null)
                    { objPatFinacialView.Dispose(); }
                }
            }
            catch (Exception ex)
            {
                e.Result = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        private void cmt_ChargeHistory_Click(object sender, EventArgs e)
        {
            tsbViewHistory_Click(sender, e);
        }

        #region "Grid Events "

        private void c1FlexGridSummary_Click(object sender, EventArgs e)
        {
            iSummarySelRow = c1FlexGridSummary.RowSel;
        }

        private void c1FlexGridChargesClaims_Click(object sender, EventArgs e)
        {
            if (c1FlexGridChargesClaims.RowSel > c1FlexGridChargesClaims.Rows.Count - 1)
            {
                iChargesSelRow = c1FlexGridChargesClaims.RowSel;
            }

        }

        private void c1FlexGridAvailResrv_Click(object sender, EventArgs e)
        {
            iReserveSelRow = c1FlexGridAvailResrv.RowSel;
        }

        private void c1FlexGridPmnt_Click(object sender, EventArgs e)
        {
            iPaymentSelRow = c1FlexGridPmnt.RowSel;
        }

        private void c1FlexGridSummary_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            //***************Resize Patient Summary grid and Total Patient Summary grid specific columns at same time ********
            //try
            //{
            //    c1FlexGridSummaryTotal.Cols[e.Col].Width = c1FlexGridSummary.Cols[e.Col].Width;

            //}
            ////*************** End ********
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    ex = null;
            //}
        }

        private void c1FlexGridSummaryTotal_AfterScroll(object sender, RangeEventArgs e)
        {
            //***************Scroll Patient Summary grid and Total Patient Summary grid at same time ********
            //try
            //{
            //    c1FlexGridSummary.ScrollPosition = c1FlexGridSummaryTotal.ScrollPosition;
            //}
            ////*************** End ********
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    ex = null;
            //}
        }

        private void c1FlexGridChargesClaims_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            //***************Resize Patient Charges & Claim grid and Total Patient Charges & Claim grid specific columns at same time ********
            try
            {

                c1FlexGridTotalChargesClaims.Cols[c1FlexGridChargesClaims.Cols[e.Col].Name].Width = c1FlexGridChargesClaims.Cols[e.Col].Width;

                //if (c1FlexGridTotalChargesClaims.Cols[e.Col].Name.ToString().ToUpper() == "SplitClaimNumber".ToUpper())
                //    c1FlexGridTotalChargesClaims.Cols[e.Col].Width = c1FlexGridChargesClaims.Cols[e.Col].Width + 19;
                //else if (c1FlexGridTotalChargesClaims.Cols[e.Col].Name.ToString() == "")
                //    c1FlexGridTotalChargesClaims.Cols[e.Col].Width = c1FlexGridChargesClaims.Cols[e.Col].Width - 19;
                //else
                //    c1FlexGridTotalChargesClaims.Cols[e.Col].Width = c1FlexGridChargesClaims.Cols[e.Col].Width;
            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1FlexGridPmnt_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            //***************Resize Patient Payment grid  and  Patient Payment total grid specific columns at same time ********
            try
            {
                c1FlexGridPmntTotal.Cols[c1FlexGridPmnt.Cols[e.Col].Name].Width = c1FlexGridPmnt.Cols[e.Col].Width;

            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1FlexGridAvailResrv_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            //***************Resize Patient Reserve grid  and  Patient Reserve total grid specific columns at same time ********
            try
            {
                c1FlexGridTotalAvailResrv.Cols[c1FlexGridAvailResrv.Cols[e.Col].Name].Width = c1FlexGridAvailResrv.Cols[e.Col].Width;

            }
            //*************** End ********
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //ex = null;
            }
        }

        private void c1FlexGridChargesClaims_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //HitTestInfo hitInfo = this.c1FlexGridChargesClaims.HitTest(e.X, e.Y);
            //GeneralNotes oNotes = null;
            //DataSet dsPatFinView = new DataSet();
            //try
            //{
            //    if ((hitInfo.Column == COL_NOTE_IMAGE) && (hitInfo.Row > 0))
            //    {
            //        dsPatFinView = GetTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionMSTID"]), Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionDetailMSTID"]));
            //        dsPatFinView.Tables[0].TableName = "Transaction";
            //        if (dsPatFinView.Tables["Transaction"].Rows.Count > 0)
            //        {
            //            oNotes = this.GetNotes(hitInfo.Row, Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionMSTID"]), Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionDetailMSTID"]));
            //            if ((oNotes != null) && (oNotes.Count > 0))
            //            {

            //                // *****************Check Claim is void or not********************************
            //               frmNotes ofrmNotes;
            //                if (Convert.ToString(this.c1FlexGridChargesClaims.Rows[hitInfo.Row]["Party"]) == "V")
            //                {
            //                    ofrmNotes = new frmNotes(this. gloPMGlobal.DatabaseConnectionString, this.gloPMGlobal.ClinicID, Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["nTransactionID"]), Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["ntransactiondetailid"]), Convert.ToInt64(this.c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionLineNo"]), oNotes);
            //                    ofrmNotes.nTransactionMasterDetailID = Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionDetailMSTID"]);
            //                    ofrmNotes.nChildTransactionID = Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionMSTID"]);
            //                    ofrmNotes.FormName = "PatientFinancialView";
            //                    ofrmNotes._IsVoidNote = true;
            //                    ofrmNotes.IsVoidShowNote = true;
            //                    ofrmNotes.nChildTransactionID = Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["nTransactionID"]);
            //                }
            //                else
            //                {
            //                    ofrmNotes = new frmNotes(this. gloPMGlobal.DatabaseConnectionString, this.gloPMGlobal.ClinicID, Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["nTransactionID"]), Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["ntransactiondetailid"]), Convert.ToInt64(this.c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionLineNo"]), oNotes);
            //                    ofrmNotes.nTransactionMasterDetailID = Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionDetailMSTID"]);
            //                    ofrmNotes.nChildTransactionID = Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionMSTID"]);
            //                    ofrmNotes.FormName = "PatientFinancialView";
            //                    ofrmNotes._IsVoidNote = false;
            //                    ofrmNotes.IsVoidShowNote = false;
            //                    ofrmNotes.nChildTransactionID = Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["nTransactionID"]);
            //                }
            //                // *****************End********************************
            //                ofrmNotes.ShowDialog();
            //                if (ofrmNotes.IsUpdated)
            //                {
            //                    DBLayer oDB = new DBLayer(this. gloPMGlobal.DatabaseConnectionString);
            //                    DBParameters oDBParameters = new DBParameters();
            //                    //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.Add, "Note added to trasanction line, Line No : " + this.UC_gloBillingTransactionLines.CurrentTransactionLine.ToString() + " ", ActivityOutCome.Success);
            //                    if (ofrmNotes.oNotes != null)
            //                    {
            //                        string sqlDelQuery = "";

            //                        sqlDelQuery = "delete from BL_Transaction_Lines_Notes where nTransactionId =" + Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionMSTID"]) + " and  nTransactionDetailId>0 and nTransactionDetailId =" + Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionDetailMSTID"]);
            //                        oDB.Connect(false);
            //                        oDB.Execute_Query(sqlDelQuery);
            //                        oDB.Disconnect();
            //                        if (ofrmNotes.oNotes.Count > 0)
            //                        {
            //                            for (int iNotesCount = 0; iNotesCount <= (ofrmNotes.oNotes.Count - 1); iNotesCount++)
            //                            {
            //                                if (((ofrmNotes.oNotes[iNotesCount].BillingNoteType == EOBPaymentSubType.Charges_BillingNote) || (ofrmNotes.oNotes[iNotesCount].BillingNoteType == EOBPaymentSubType.Charges_InternalNote)) || (ofrmNotes.oNotes[iNotesCount].BillingNoteType == EOBPaymentSubType.Charges_StatementNote))
            //                                {
            //                                    oDBParameters.Clear();
            //                                    oDBParameters.Add("@nTransactionID", ofrmNotes.oNotes[iNotesCount].TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                    oDBParameters.Add("@nLineNo", ofrmNotes.oNotes[iNotesCount].TransactionLineId, ParameterDirection.Input, SqlDbType.BigInt);
            //                                    oDBParameters.Add("@nTransactionDetailID", ofrmNotes.oNotes[iNotesCount].TransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                    oDBParameters.Add("@nNoteType", ofrmNotes.oNotes[iNotesCount].NoteType, ParameterDirection.Input, SqlDbType.Int);
            //                                    oDBParameters.Add("@nNoteId", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
            //                                    oDBParameters.Add("@nNoteDateTime", ofrmNotes.oNotes[iNotesCount].NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
            //                                    oDBParameters.Add("@nUserID", ofrmNotes.oNotes[iNotesCount].UserID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                    oDBParameters.Add("@sNoteDescription", ofrmNotes.oNotes[iNotesCount].NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
            //                                    oDBParameters.Add("@nClinicID", ofrmNotes.oNotes[iNotesCount].ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            //                                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0L), ParameterDirection.Input, SqlDbType.BigInt);
            //                                    oDBParameters.Add("@nBillingNoteType", ofrmNotes.oNotes[iNotesCount].BillingNoteType, ParameterDirection.Input, SqlDbType.Int);
            //                                    oDBParameters.Add("@nCloseDate", gloDateMaster.gloDate.DateAsNumber(this.c1FlexGridChargesClaims.Rows[hitInfo.Row]["nCloseDate"].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
            //                                    object objectID = null;
            //                                    oDB.Connect(false);
            //                                    oDB.Execute("BL_INUP_Transaction_Lines_Notes", oDBParameters, out objectID);
            //                                    oDB.Disconnect();
            //                                    if ((objectID != null) && (ofrmNotes.oNotes[iNotesCount].TransactionDetailID != 0L))
            //                                    {
            //                                        ofrmNotes.oNotes[iNotesCount].NoteID = (long)objectID;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                        else
            //                        {
            //                            string sqlDeleteQuery = "";

            //                            sqlDeleteQuery = "delete from BL_Transaction_Lines_Notes where nTransactionId =" + Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionMSTID"]) + " and nTransactionDetailId =" + Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionDetailMSTID"]);

            //                            oDB.Connect(false);
            //                            oDB.Execute_Query(sqlDeleteQuery);
            //                            oDB.Disconnect();
            //                        }
            //                    }
            //                }
            //                ofrmNotes.Dispose();
            //                tbPatientFinancial_SelectedIndexChanged(sender, e);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            //}
            Boolean _IsModified = false;
            try
            {
                HitTestInfo hitInfo = this.c1FlexGridChargesClaims.HitTest(e.X, e.Y);
                if (c1FlexGridChargesClaims.Rows.Count > 1)
                {
                    if (hitInfo.Row != 0)
                    {
                        _IsModified = ModifyCharge();
                        if (_IsModified)
                        {
                            oPatientControl.RefreshData();
                            RemoveTabPageTag();
                            FillClaim_Charges();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Claim not available.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1FlexGridAvailResrv_MouseDoubleClick(object sender1066, MouseEventArgs e)
        {
            try
            {
                int iRowIndex = 0;
                iRowIndex = c1FlexGridAvailResrv.HitTest(e.X, e.Y).Row;
                if (iRowIndex > 0)
                {
                    OpenReserveForModify(iRowIndex);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1FlexGrid_MouseClick(object sender, MouseEventArgs e)
        {
            //*********************Enable Void Payment Button only if click on payment grid row**********
            if (((C1.Win.C1FlexGrid.C1FlexGrid)sender).Name == "c1FlexGridPmnt")
            {
                //ts_VoidPayment.Enabled = true;
                tsbPatientPmnt.Enabled = true;
                tsb_PatRefund.Enabled = false;
                tsb_ShowDetails.Enabled = false;
                tsb_ViewPatRefund.Enabled = false;
            }
            else if (((C1.Win.C1FlexGrid.C1FlexGrid)sender).Name == "c1FlexGridAvailResrv")
            {
                //ts_VoidPayment.Enabled = false;
                tsb_ShowDetails.Enabled = true;
                tsb_PatRefund.Enabled = true;
                tsbPatientPmnt.Enabled = false;
                tsb_ViewPatRefund.Enabled = false;

            }
            else if (((C1.Win.C1FlexGrid.C1FlexGrid)sender).Name == "c1PatientRefund")
            {
                //ts_VoidPayment.Enabled = false;
                tsb_ViewPatRefund.Enabled = true;
                tsbPatientPmnt.Enabled = false;
                tsb_PatRefund.Enabled = false;
                tsb_ShowDetails.Enabled = false;
            }
        }

        private void c1FlexGridChargesClaims_MouseDown(object sender, MouseEventArgs e)
        {
            if (c1FlexGridChargesClaims.HitTest(e.X, e.Y).Row >= 1 && c1FlexGridChargesClaims.HitTest(e.X, e.Y).Column != COL_NOTE_IMAGE)
            {
                Int32 tempRow = 0;
                Int64 tempTaskId = 0;
                tempRow = c1FlexGridChargesClaims.HitTest(e.X, e.Y).Row;
                c1FlexGridChargesClaims.Row = tempRow;
                if (e.Button == MouseButtons.Right)
                {
                    tempTaskId = Convert.ToInt64(c1FlexGridChargesClaims.GetData(tempRow, COL_TRANSACTION_MST_ID));
                    if (tempTaskId > 0)
                    {
                        c1FlexGridChargesClaims.ContextMenuStrip = contextMenuChargeHistory;
                    }//if (tempTaskId > 0)
                    else
                    {
                        c1FlexGridChargesClaims.ContextMenuStrip = null;
                    }
                }
                else
                {
                    c1FlexGridChargesClaims.ContextMenuStrip = null;
                }
            }//if (c1ViewTask.HitTest(e.X, e.Y)
            else
            {
                c1FlexGridChargesClaims.ContextMenuStrip = null;
            }
        }

        private void c1PatientRefund_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            try
            {
                c1RefundTotal.Cols[e.Col].Width = c1PatientRefund.Cols[e.Col].Width;

            }
            //*************** End ********
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //ex = null;
            }
        }

        private void c1RefundTotal_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            //try
            //{
            //    c1PatientRefund.Cols[c1RefundTotal.Cols[e.Col].Name].Width = c1RefundTotal.Cols[e.Col].Width;

            //}
            ////*************** End ********
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    ex = null;
            //}
        }

        private void c1FlexGridChargesClaims_MouseMove(object sender, MouseEventArgs e)
        {
            if (c1FlexGridChargesClaims.HitTest(e.X, e.Y).Column >= 14 && c1FlexGridChargesClaims.HitTest(e.X, e.Y).Column <= 27)
            {
                gloBilling.gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
            }
            else
            {
                gloBilling.gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
            }
        }

        private void c1FlexGridPmnt_AfterScroll(object sender, RangeEventArgs e)
        {
            //***************Scroll Patient Summary grid and Total Patient Summary grid at same time ********
            try
            {
                c1FlexGridPmntTotal.ScrollPosition = c1FlexGridPmnt.ScrollPosition;
            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1FlexGridAvailResrv_AfterScroll(object sender, RangeEventArgs e)
        {
            //***************Scroll Patient Summary grid and Total Patient Summary grid at same time ********
            try
            {
                c1FlexGridTotalAvailResrv.ScrollPosition = c1FlexGridAvailResrv.ScrollPosition;
            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1FlexGridChargesClaims_AfterScroll(object sender, RangeEventArgs e)
        {
            //***************Scroll Patient Summary grid and Total Patient Summary grid at same time ********
            try
            {
                c1FlexGridTotalChargesClaims.ScrollPosition = c1FlexGridChargesClaims.ScrollPosition;
            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1PatientRefund_AfterScroll(object sender, RangeEventArgs e)
        {
            try
            {
                c1RefundTotal.ScrollPosition = c1PatientRefund.ScrollPosition;
            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1PatientRefund_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (c1PatientRefund.Rows.Count > 1)
                {
                    object nrefundid = null;
                    Int64 nPatientID = 0;

                    nrefundid = c1PatientRefund.GetData(c1PatientRefund.RowSel, 0);
                    nPatientID = Convert.ToInt64(c1PatientRefund.GetData(c1PatientRefund.RowSel, 11));
                    frmPatientPayRefundViewV2 ofrmPatientPayRefundView = new frmPatientPayRefundViewV2(gloPMGlobal.DatabaseConnectionString, nPatientID, Convert.ToInt64(nrefundid));
                    ofrmPatientPayRefundView.ShowDialog(this);
                    ofrmPatientPayRefundView.Dispose();

                    oPatientControl.RefreshBalances();
                    FillPatientReserves();
                    FillPatientRefunds();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void c1PatientRefund_MouseMove(object sender, MouseEventArgs e)
        {
            gloBilling.gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1FlexGridAvailResrv_MouseMove(object sender, MouseEventArgs e)
        {
            gloBilling.gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1FlexGridPmnt_MouseMove(object sender, MouseEventArgs e)
        {
            gloBilling.gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        //Added By Shweta -20100812
        private void c1FlexGrid_Statements_DoubleClick(object sender, EventArgs e)
        {
            if (c1FlexGrid_Statements.Rows.Count > 1)
            {
                _nTrasactionID = Convert.ToInt64(c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, 8));
                ViewPatientStatementTemplate();

            }
        }

        private void c1FlexGrid_Statements_CellButtonClick(object sender, RowColEventArgs e)
        {
            string _voidUserName = "";
            string _voidNotes = "";
            DateTime _voidDateTime = new DateTime();
            DataSet dsPatFinView = new DataSet();
           // int _rowIndex = 0;
            int _xCord = 0;
            int _yCord = 0;
            int _Height = 0;
            _Height = c1FlexGrid_Statements.Rows[0].Height + 10;
            DataTable dt = new DataTable();
            Point defPnt = new Point();
            GetCursorPos(ref defPnt);
            _xCord = defPnt.X;
            _yCord = defPnt.Y;
            Int64 _nPatientIDStmt = 0;
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oParameters = null;
            try
            {
                //string sqlQuery = "";

                //sqlQuery = "SELECT CASE BL_Batch_PatientStatement_Mst.bIsUnclosedDay  WHEN 1 THEN CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) + ' [Unclosed] '  ELSE CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) END AS dtStatementDate,Convert(varchar,BL_Batch_PatientStatement_Mst.dtCreateDate,101) As dtCreateDate,ISNULL(BL_Batch_PatientStatement_Mst.sBatchName,'') as sBatchName, " +
                //          "ISNULL(BL_Batch_PatientStatement_Mst.sUserName,'') as sUserName, ISNULL(Patient.sFirstName,'') as sFirstName, ISNULL(Patient.sMiddleName,'') as sMiddleName, ISNULL(Patient.sLastName,'') as sLastName, " +
                //          "ISNULL(BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID,0) as nBatchPateintStatMstID,ISNULL(BL_Batch_PatientStatement_DTL.nTempleteTransactionID,0) as nTempleteTransactionID, ISNULL(Patient.nPatientID,0) as nPatientID,sVoidNotes As Notes,CASE WHEN isnull(BL_Batch_PatientStatement_DTL.bIsVoid,0) = 0 then ' ' ELSE 'Voided' END As Status,BL_Batch_PatientStatement_DTL.nBatchPateintStatDtlID, " +
                //          "ISNULL(BL_Batch_PatientStatement_DTL.nVoidUserId,0) AS UserID,ISNULL(BL_Batch_PatientStatement_DTL.sVoidUserName,'') AS UserName,ISNULL(BL_Batch_PatientStatement_DTL.dtVoidDate,0) AS VoidDateTime FROM Patient  WITH (NOLOCK) INNER JOIN " +
                //          "BL_Batch_PatientStatement_DTL WITH (NOLOCK) ON Patient.nPatientID = BL_Batch_PatientStatement_DTL.nPatientID INNER JOIN " +
                //          " BL_Batch_PatientStatement_Mst WITH (NOLOCK) ON BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID = BL_Batch_PatientStatement_Mst.nBatchPateintStatMstID where Patient.nPatientID=" + _nPatientID + " order by dtCreateDate desc";
                //SqlDataAdapter adpt = new SqlDataAdapter(sqlQuery, _con);
                //adpt.Fill(dsPatFinView);
                if (oPatientControl.IsAllAccPatSelected == true)
                { _nPatientIDStmt = 0; }
                oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                oParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oParameters.Add("@nPAccountID", _nPAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", _nPatientIDStmt, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Fill_PatientStatement", oParameters, out dsPatFinView);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //if (_con != null) { _con.Close(); }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            if (dsPatFinView != null)
                dt = dsPatFinView.Tables[0];


            DataRow[] result = dt.Select("nTempleteTransactionID =  " + c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, 8) + "");

       

            if (result!=null)
            {
                _voidDateTime = (DateTime)result[0].ItemArray.GetValue(8);
                _voidUserName = Convert.ToString(result[0].ItemArray.GetValue(7));
                _voidNotes = Convert.ToString(result[0].ItemArray.GetValue(9));
            }

            frmVoidStatementInfoDialog objVoidNotes = new frmVoidStatementInfoDialog(_voidDateTime, _voidUserName, _voidNotes, _xCord, _yCord + _Height);
            objVoidNotes.ShowDialog(this);
            objVoidNotes.Dispose();
            objVoidNotes = null;
        }

        private void c1Reserve_AfterScroll(object sender, RangeEventArgs e)
        {
            //***************Scroll Patient Summary grid and Total Patient Summary grid at same time ********
            try
            {
                c1InsReserveTotal.ScrollPosition = c1Reserve.ScrollPosition;
            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1InsuranceRefundLog_AfterScroll(object sender, RangeEventArgs e)
        {
            //***************Scroll Patient Summary grid and Total Patient Summary grid at same time ********
            try
            {
                c1InsRefundTotal.ScrollPosition = c1InsuranceRefundLog.ScrollPosition;
            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1Reserve_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            //***************Resize Patient Payment grid  and  Patient Payment total grid specific columns at same time ********
            try
            {
                c1InsReserveTotal.Cols[e.Col].Width = c1Reserve.Cols[e.Col].Width;

            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1InsuranceRefundLog_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            try
            {
                c1InsRefundTotal.Cols[c1InsuranceRefundLog.Cols[e.Col].Name].Width = c1InsuranceRefundLog.Cols[e.Col].Width;

            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1Reserve_MouseMove(object sender, MouseEventArgs e)
        {
            gloBilling.gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1InsuranceRefundLog_MouseMove(object sender, MouseEventArgs e)
        {
            gloBilling.gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1InsuranceRefundLog_Click(object sender, EventArgs e)
        {

        }

        private void c1GlobalPeriods_MouseMove(object sender, MouseEventArgs e)
        {
            gloBilling.gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1GlobalPeriods_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HitTestInfo hitInfo = this.c1GlobalPeriods.HitTest(e.X, e.Y);
            if (c1GlobalPeriods.Rows.Count > 1)
            {
                if (hitInfo.Row != 0)
                {
                    gloBilling.frmSetupModifyGlobalPeriod ofrmSetupGlobalPeriod = new gloBilling.frmSetupModifyGlobalPeriod(Convert.ToInt64(c1GlobalPeriods.GetData(c1GlobalPeriods.RowSel, "ID")));
                    ofrmSetupGlobalPeriod.ShowDialog(this);
                    ofrmSetupGlobalPeriod.Dispose();
                    FillGlobalPeriods();
                }
            }
        }



        #endregion

        #region "PatientStrip Events"

        private void oPatientControl_PatientChanged(object sender, EventArgs e)
        {
            try
            {
                //Code added by SaiKrishna date:02-14-2011
                RemoveTabPageTag();
                //if (oPatientControl.IsAllAccPatSelected)
                //{
                //    _nPatientID = 0;
                //    _nSelectedPatientId = 0;
                //}
                //else
                //{
                //    _nPatientID = oPatientControl.PatientID;
                //    _nSelectedPatientId = oPatientControl.CmbSelectedPatientID;
                //}
                _nPatientID = oPatientControl.PatientID;
                _nSelectedPatientId = oPatientControl.CmbSelectedPatientID;
                _nPAccountId = oPatientControl.PAccountID; ;
                _nAccountPatientId = oPatientControl.AccountPatientID;
                _nGuarantorId = oPatientControl.GuarantorID;
                //_blnShowZeroBal = false;

                //if (this.tbPatientFinancial.SelectedTab.Name != "tbpgAccountLog")
                //{
                    tbPatientFinancial_SelectedIndexChanged(null, null);
                //}

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void oPatientControl_OnAccountChanged(object sender, EventArgs e)
        {

            //Code added by SaiKrishna date:02-14-2011
            RemoveTabPageTag();
            //if (oPatientControl.IsAllAccPatSelected)
            //{
            //    _nPatientID = 0;
            //    _nSelectedPatientId = 0;
            //}
            //else
            //{
            //    _nPatientID = oPatientControl.PatientID;
            //    _nSelectedPatientId = oPatientControl.CmbSelectedPatientID;
            //}

            _nPatientID = oPatientControl.PatientID;
            _nSelectedPatientId = oPatientControl.CmbSelectedPatientID;
            _nPAccountId = oPatientControl.PAccountID; ;
            _nAccountPatientId = oPatientControl.AccountPatientID;
            _nGuarantorId = oPatientControl.GuarantorID;
            //_blnShowZeroBal = false;
            tbPatientFinancial_SelectedIndexChanged(null, null);
            //tsb_Refresh_Click(null, null);
        }

        //private void oPatientControl_OnAccountPatientChanged(object sender, EventArgs e)
        //{
        //    //Code added by SaiKrishna date:02-14-2011
        //    RemoveTabPageTag();
        //    //if (oPatientControl.IsAllAccPatSelected)
        //    //{
        //    //    _nPatientID = 0;
        //    //    _nSelectedPatientId = 0;
        //    //}
        //    //else
        //    //{
        //    //    _nPatientID = oPatientControl.PatientID;
        //    //    _nSelectedPatientId = oPatientControl.CmbSelectedPatientID;
        //    //}

        //    _nPatientID = oPatientControl.PatientID;
        //    _nSelectedPatientId = oPatientControl.CmbSelectedPatientID;
        //    _nPAccountId = oPatientControl.PAccountID;
        //    _nAccountPatientId = oPatientControl.AccountPatientID;
        //    _nGuarantorId = oPatientControl.GuarantorID;
        //    //tsb_Refresh_Click(null, null);
        //    //_blnShowZeroBal = false;

        //    if (this.tbPatientFinancial.SelectedTab.Name != "tbpgAccountLog")
        //    {
        //        tbPatientFinancial_SelectedIndexChanged(null, null);
        //    }

        //}

        void oPatientControl_ControlSize_Changed(object sender, EventArgs e)
        {
            //try
            //{
            //    pnlPatientStrip.Height = oPatientControl.Height;

            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    ex = null;
            //}
        }

        void oPatientControl_OnPatientSearchKeyPress(object sender, EventArgs e)
        {
            //if (oPatientControl.PatientID > 0)
            //{
            RemoveTabPageTag();
            //if (oPatientControl.IsAllAccPatSelected)
            //{
            //    _nPatientID = 0;
            //}
            //else
            //{
            //    _nPatientID = oPatientControl.PatientID;
            //}
                _nPatientID = oPatientControl.PatientID;
            //tsb_Refresh_Click(null, null);
            //_blnShowZeroBal = false;
            tbPatientFinancial_SelectedIndexChanged(null, null);
            //}
        }

        #endregion "PatientStrip Events"

        #endregion

        #region "Function"

        private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        {
            try
            {
                // Bug# 48050 - when we hold a insurance plan from PatAcct >> modify patient from banner then it will not refresh the grid.
             //   oPatientControl.OnInsurancePlanHoldChanged += new gloStripControl.gloPatientStrip_FA.InsurancePlanHoldChanged(oPatientControl_InsurancePlanHoldChanged);

                oPatientControl.IsAllAccPatSelected = true;

                if (_nSelectAccountId != 0)
                {
                    oPatientControl.FillDetails(PatientId,_nSelectAccountId ,gloStripControl.FormName.PatientAccountView);
                }
                else
                {
                    oPatientControl.FillDetails(PatientId, gloStripControl.FormName.PatientAccountView, PatientProviderId, false);
                }

                _nPatientID = oPatientControl.PatientID;
                _nSelectedPatientId = oPatientControl.CmbSelectedPatientID;
                _nPAccountId = oPatientControl.PAccountID; ;
                _nAccountPatientId = oPatientControl.AccountPatientID;
                _nGuarantorId = oPatientControl.GuarantorID;
               // oPatientControl_InsurancePlanHoldChanged();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        //// Bug# 48050 - when we hold a insurance plan from PatAcct >> modify patient from banner then it will not refresh the grid.
        private void oPatientControl_InsurancePlanHoldChanged()
        {
            FillClaimOnHold();
        }

        private DataSet fillgridData(string spName, DBParameters oParameters, DBLayer oDB)
        {
            if (oParameters.Count > 0)
            {
                oParameters.Clear();
            }
            DataSet dsdata = new DataSet();
            try
            {
                //addded by saikrishna on 11-may-2011.
                if (spName != "gSP_GET_Chronology")
                {
                    oParameters.Add("@nPAccountID", _nPAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                }
                oParameters.Add("@nPatientID", _nSelectedPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive(spName, oParameters, out dsdata);
                oDB.Disconnect();
                return dsdata;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;

            }
        }

        private void setGridStyle(C1FlexGrid C1Flex, Int32 iRowNumber, Int32 iColNumber, Int32 iColCount)
        {
            CellStyle csSubTotalRow;
            CellStyle csSubCol;
           // csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
            try
            {
                if (C1Flex.Styles.Contains("SubTotalRow"))
                {
                    csSubTotalRow = C1Flex.Styles["SubTotalRow"];
                }
                else
                {
                    csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
                    //csSubTotalRow.DataType = typeof(System.Decimal);
                    csSubTotalRow.Format = "c";
                    csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                    csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                    csSubTotalRow.ForeColor = Color.Blue;
                    csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;
                }

            }
            catch
            {
                csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
                //csSubTotalRow.DataType = typeof(System.Decimal);
                csSubTotalRow.Format = "c";
                csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                csSubTotalRow.ForeColor = Color.Blue;
                csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;
            }
      
          //  csSubCol = C1Flex.Styles.Add("SubCol");
            try
            {
                if (C1Flex.Styles.Contains("SubCol"))
                {
                    csSubCol = C1Flex.Styles["SubCol"];
                }
                else
                {
                    csSubCol = C1Flex.Styles.Add("SubCol");
                    csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                    csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                    csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubCol.TextEffect = TextEffectEnum.Flat;
                    csSubCol.ForeColor = Color.Maroon;
                }

            }
            catch
            {
                csSubCol = C1Flex.Styles.Add("SubCol");
                csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubCol.TextEffect = TextEffectEnum.Flat;
                csSubCol.ForeColor = Color.Maroon;
            }
   
        
            CellRange subTotalRange;
            subTotalRange = C1Flex.GetCellRange(iRowNumber, 0, iRowNumber, iColCount);
            subTotalRange.Style = csSubTotalRow;
            CellRange subCol;
            subCol = C1Flex.GetCellRange(iRowNumber, iColNumber, iRowNumber, iColNumber);
            subCol.Style = csSubCol;
        }

        public GeneralNotes GetNotes(int iTransactionLineNumber, Int64 nTransactionID, Int64 nTransactionDetailID)
        {
            GeneralNote oNote = new GeneralNote();
            GeneralNotes oNotes = new GeneralNotes();
            gloBilling.EOBPaymentSubType BillingNoteType = gloBilling.EOBPaymentSubType.None;
            gloBilling.NoteType blNoteType = gloBilling.NoteType.GeneralNote;
           // gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(this._nPatientID, _nPAccountId);
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            }

            DataSet dsNotes = new DataSet();
            objPatFinacialView.GetChargeNotes(nTransactionID, nTransactionDetailID, out dsNotes);
            try
            {
                if (dsNotes.Tables[0].Rows.Count > 0)
                {
                    for (int iNotesCnt = 0; iNotesCnt <= (dsNotes.Tables[0].Rows.Count - 1); iNotesCnt++)
                    {
                        if (Convert.ToString(dsNotes.Tables[0].Rows[iNotesCnt]["sNoteDescription"]) != "")
                        {
                            oNote = new GeneralNote();
                            switch (Convert.ToInt32((dsNotes.Tables[0].Rows[iNotesCnt]["nBillingNoteType"] == DBNull.Value) ? 0 : dsNotes.Tables[0].Rows[iNotesCnt]["nBillingNoteType"]))
                            {
                                case 17:
                                    BillingNoteType = gloBilling.EOBPaymentSubType.Charges_BillingNote;
                                    break;

                                case 18:
                                    BillingNoteType = gloBilling.EOBPaymentSubType.Charges_StatementNote;
                                    break;

                                case 19:
                                    BillingNoteType = gloBilling.EOBPaymentSubType.Charges_InternalNote;
                                    break;

                                case 0:
                                    BillingNoteType = gloBilling.EOBPaymentSubType.None;
                                    break;
                            }
                            int nNote = Convert.ToInt32(dsNotes.Tables[0].Rows[iNotesCnt]["nNoteType"].ToString());
                            if (nNote == 0)
                            {
                                blNoteType = gloBilling.NoteType.GeneralNote;
                            }
                            if (nNote == 10)
                            {
                                blNoteType = gloBilling.NoteType.Void_Note;
                            }
                            oNote.NoteDescription = dsNotes.Tables[0].Rows[iNotesCnt]["sNoteDescription"].ToString();
                            oNote.TransactionDetailID = Convert.ToInt64(dsNotes.Tables[0].Rows[iNotesCnt]["nTransactionDetailID"]);
                            oNote.TransactionID = Convert.ToInt64(dsNotes.Tables[0].Rows[iNotesCnt]["nTransactionID"]);
                            oNote.BillingNoteType = BillingNoteType;
                            oNote.NoteID = Convert.ToInt64(dsNotes.Tables[0].Rows[iNotesCnt]["nNoteID"]);
                            oNote.NoteDate = Convert.ToInt32(dsNotes.Tables[0].Rows[iNotesCnt]["nNoteDateTime"]);
                            oNote.NoteType = blNoteType;
                            oNote.ClinicID = Convert.ToInt32(dsNotes.Tables[0].Rows[iNotesCnt]["nClinicID"]);
                            oNote.UserID = Convert.ToInt64(dsNotes.Tables[0].Rows[iNotesCnt]["nUserID"]);
                            oNote.TransactionLineId = Convert.ToInt32(dsNotes.Tables[0].Rows[iNotesCnt]["nLineno"]);

                        }
                        oNotes.Add(oNote);
                    }
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }

            finally
            {
                if (objPatFinacialView != null)
                {
                    objPatFinacialView.Dispose();
                }
            }
            return oNotes;
        }

        private DataSet GetTransactionID(Int64 nMstTransactionID, Int64 nMstTransactionDetailID)
        {
            DataSet dsTransaction = new DataSet();
            DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            oParameters.Add("@nMasterTransactionID", nMstTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
            oParameters.Add("@nMasterTransactionDetailID", nMstTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);

            oDB.Connect(false);
            oDB.Retrive("BL_SELECT_TransactionDetail", oParameters, out dsTransaction);
            oDB.Disconnect();
            return dsTransaction;
        }

        private Boolean ModifyCharge()
        {
            Boolean _IsModified = false;

            try
            {
                //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(this._nPatientID);
                gloPatientFinancialViewV2 objPatFinacialView = null;
                if (oPatientControl.IsAllAccPatSelected)
                {
                    objPatFinacialView = new gloPatientFinancialViewV2(0);
                }
                else
                {
                    objPatFinacialView = new gloPatientFinancialViewV2(this._nPatientID);
                }
                Int64 ParamTransactionId = 0;
                Int64 PatientID = 0;
                //Int64 _ParamClaimNo = 0;
                gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");

                if (Convert.ToString(tbPatientFinancial.SelectedTab.Text).ToUpper() == _strTagClaims.ToUpper())
                {


                    if (c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionMSTID"].Index) != null && Convert.ToString(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionMSTID"].Index)) != "")
                    {
                        DataSet dsPatFinView = new DataSet();
                        //dsPatFinView = GetTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionMSTID"].Index)), Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionDetailMSTID"].Index)));
                        //DBLayer oDB = new DBLayer(this._DatabaseConnectionString);
                        //DBParameters oParameters = new DBParameters();
                        //oParameters.Add("@nMasterTransactionID", c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionMSTID"].Index), ParameterDirection.Input, SqlDbType.BigInt);
                        //oParameters.Add("@nMasterTransactionDetailID", c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionDetailMSTID"].Index), ParameterDirection.Input, SqlDbType.BigInt);

                        //oDB.Connect(false);
                        //oDB.Retrive("BL_SELECT_TransactionDetail", oParameters, out dsPatFinView);
                        //oDB.Disconnect();

                        //if (dsPatFinView.Tables[0].Rows.Count > 0)
                        //    ParamTransactionId = Convert.ToInt64(dsPatFinView.Tables[0].Rows[0]["nTransactionID"]);
                        //else
                        //    ParamTransactionId = 0;
                        //_ParamClaimNo = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols[COL_11].Index));

                        //Int64 _ModifyClaimTransactionID = 0;
                        if (Convert.ToInt32(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-")) >= 0)
                        {
                            if (Convert.ToInt32(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["bIsVoid"].Index)) == 1)
                            {
                                //ParamTransactionId = GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)), "", true);
                                ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(0, c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-"))), c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-") + 1), true);
                                //_ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "", true);
                                PatientID = Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["PatientID"].Index));
                                _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, true, true, this);
                            }
                            else
                            {
                                ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(0, c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-"))), c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-") + 1), false);
                                //_ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "", false);
                                PatientID = Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["PatientID"].Index));
                                if ( ParamTransactionId != 0)
                                    _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, false, true, this);
                            }
                            //if (ParamTransactionId > 0 && _nPatientID > 0)
                            //{

                            //    //DataTable _dtIsVoid = CheckIsVoided(_ParamTransactionId);
                            //    //if (_dtIsVoid != null && _dtIsVoid.Rows.Count > 0)
                            //    //{
                            //    if (Convert.ToInt32(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["bIsVoid"].Index)) == 1)
                            //    {
                            //        //_ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "", true);
                            //        ogloBilling.ShowModifyCharges(_nPatientID, Convert.ToInt64(dsPatFinView.Tables[0].Rows[0]["nTransactionID"]), true);
                            //    }
                            //    else
                            //    {
                            //        //_ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "", false);
                            //        ogloBilling.ShowModifyCharges(_nPatientID, Convert.ToInt64(dsPatFinView.Tables[0].Rows[0]["nTransactionID"]));
                            //    }
                            //    //}

                            //}
                        }
                        else
                        {

                            if (Convert.ToInt32(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["bIsVoid"].Index)) == 1)
                            {
                                ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)), "", true);
                                //_ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "", true);
                                PatientID = Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["PatientID"].Index));
                                _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, true, true, this);
                            }
                            else
                            {
                                ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)), "", false);
                                //_ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "", false);
                                PatientID = Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["PatientId"].Index));
                                if ( ParamTransactionId != 0)
                                    _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, false, true, this);
                            }
                        }
                    }
                }
                ogloBilling.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            return _IsModified;
        }

        private void FillInsuranceReserves()
        {
            DataTable _dtReserves = new DataTable();
            
            //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            }
            try
            {
                //DesignReserveGrid();
                DesignReserveGrid(c1Reserve);
                _dtReserves = objPatFinacialView.FillInsReservesAssociatedWithPatient();
                Decimal _dTotalToRes = 0;
                Decimal _dTotalAvlRes = 0;
                if (_dtReserves != null && _dtReserves.Rows.Count > 0)
                {
                    int _rowIndex = 0;

                    for (int i = 0; i < _dtReserves.Rows.Count; i++)
                    {

                        #region " Set Data "

                        _rowIndex = c1Reserve.Rows.Add().Index;

                        c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"]));
                        //c1Reserve.SetData(_rowIndex, COL_EOBID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBID"]));
                        //c1Reserve.SetData(_rowIndex, COL_EOBDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBDtlID"]));
                        //c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]));
                        //c1Reserve.SetData(_rowIndex, COL_BLTRANSACTIONID, Convert.ToInt64(_dtReserves.Rows[i]["nBillingTransactionID"]));
                        //c1Reserve.SetData(_rowIndex, COL_BLTRANDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nBillingTransactionDetailID"]));
                        //c1Reserve.SetData(_rowIndex, COL_BLTRANLINEID, Convert.ToString(_dtReserves.Rows[i]["nBillingTransactionLineNo"]));
                        //c1Reserve.SetData(_rowIndex, COL_DOSFROM, Convert.ToInt64(_dtReserves.Rows[i]["nDOSFrom"]));
                        //c1Reserve.SetData(_rowIndex, COL_DOSTO, Convert.ToString(_dtReserves.Rows[i]["nDOSTo"]));
                        //c1Reserve.SetData(_rowIndex, COL_PATIENTID, Convert.ToString(_dtReserves.Rows[i]["nPatientID"]));
                        //c1Reserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(_dtReserves.Rows[i]["PatientName"]));//Patient or Insurance Name
                        c1Reserve.SetData(_rowIndex, COL_COMPANYNAME, Convert.ToString(_dtReserves.Rows[i]["InsuarnceCompanyName"]));
                        string _originalPayment = "";
                        _originalPayment = ((gloBilling.PaymentMode)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])).ToString() + "# " + Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]) + " " + String.Format("{0:MM/dd/yyyy}", _dtReserves.Rows[i]["nCheckDate"]) + " $ " + Convert.ToDecimal(_dtReserves.Rows[i]["nCheckAmount"]);
                        c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount
                        c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]));
                        c1Reserve.SetData(_rowIndex, COL_TYPE, ((gloBilling.EOBPaymentSubType)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                        c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]));//Note
                        c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(_dtReserves.Rows[i]["UsedReserve"]));//Used amount
                        c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount
                        c1Reserve.SetData(_rowIndex, COL_REFUND, 0);//Current amount to use from avaiable amount

                        c1Reserve.SetData(_rowIndex, COL_PAYMODE, ((gloBilling.PaymentMode)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])));
                        //c1Reserve.SetData(_rowIndex, COL_REFEOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentID"]));
                        //c1Reserve.SetData(_rowIndex, COL_REFEOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentDetailID"]));
                        //c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"]));
                        //c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]));
                        //c1Reserve.SetData(_rowIndex, COL_ACCOUNTID, Convert.ToInt64(_dtReserves.Rows[i]["nAccountID"]));
                        //c1Reserve.SetData(_rowIndex, COL_ACCOUNTTYPE, Convert.ToInt32(_dtReserves.Rows[i]["nAccountType"]));
                        //c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTID, Convert.ToInt64(_dtReserves.Rows[i]["nMSTAccountID"]));
                        //c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTTYPE, Convert.ToInt32(_dtReserves.Rows[i]["nMSTAccountType"]));

                        //c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentID"]));
                        //c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentDetailID"]));
                        c1Reserve.SetData(_rowIndex, COL_PAYMENTCLOSEDATE, String.Format("{0:MM/dd/yyyy}", _dtReserves.Rows[i]["nCloseDate"]));
                        c1Reserve.SetData(_rowIndex, COL_PAYMENTMODE, Convert.ToInt64(_dtReserves.Rows[i]["nPayMode"]));
                        c1Reserve.SetData(_rowIndex, COL_PAYMENTMODENO, Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]));
                        c1Reserve.SetData(_rowIndex, COL_USERNAME, Convert.ToString(_dtReserves.Rows[i]["sUserName"]));
                        c1Reserve.SetData(_rowIndex, COL_ASSO_CLAIMNO, Convert.ToString(_dtReserves.Rows[i]["AssociationClaim"]));

                        c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTID, Convert.ToInt64(_dtReserves.Rows[i]["AssociationPatientID"]));
                        c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTNAME, Convert.ToString(_dtReserves.Rows[i]["AssociationPatient"]));
                        c1Reserve.SetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(_dtReserves.Rows[i]["AssociationMSTTransactionID"]));
                        c1Reserve.SetData(_rowIndex, COL_ASSO_TRACKTRANSACTIONID, Convert.ToInt64(_dtReserves.Rows[i]["AssociationnTransactionID"]));


                        _dTotalToRes = _dTotalToRes + Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]);
                        _dTotalAvlRes = _dTotalAvlRes + Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]);

                        #region " Set Styles "

                        c1Reserve.SetCellStyle(_rowIndex, COL_REFUND, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

                        #endregion " Set Styles "


                        #endregion
                    }
                    DesignReserveGrid(c1InsReserveTotal);
                    setGridStyle(c1InsReserveTotal, 0, COL_PAYMENTCLOSEDATE, c1InsReserveTotal.Cols.Count - 1);
                    c1InsReserveTotal.SetData(0, COL_PAYMENTCLOSEDATE, "Total :");
                    c1InsReserveTotal.SetData(0, COL_TORESERVES, "$" + _dTotalToRes);
                    c1InsReserveTotal.SetData(0, COL_AVAILABLE, "$" + _dTotalAvlRes);
                    c1InsReserveTotal.Width = c1Reserve.Width;
                }
                else
                {
                    c1InsReserveTotal.Rows.Count = 0;
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
                //_IsFormLoading = false;
            }
        }

        //Added By Shweta 20112406
        private void FillPatientPayment()
        {
            //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);
            }
            DataSet dsPayment = new DataSet();
            try
            {
                #region "Payment"

                objPatFinacialView.GetPatientPayment(out dsPayment);
                this.c1FlexGridPmnt.DataMember = "Payments";
                this.c1FlexGridPmnt.DataSource = dsPayment;
                c1FlexGridPmnt.ShowCellLabels = false;

                if (dsPayment.Tables["Payments"].Rows.Count > 0)
                {
                    tsbPatientPmnt.Enabled = true;
                    this.c1FlexGridPmntTotal.Visible = true;
                    this.c1FlexGridPmntTotal.Rows[0].Visible = false;

                    this.c1FlexGridPmntTotal.DataMember = "TotalPayments";
                    this.c1FlexGridPmntTotal.DataSource = dsPayment;
                    setGridStyle(c1FlexGridPmntTotal, 1, 8, 14);
                    if (dsPayment.Tables["Payments"].Rows.Count >= iPaymentSelRow)
                        c1FlexGridPmnt.Row = iPaymentSelRow;
                }
                else
                {
                    this.c1FlexGridPmntTotal.DataMember = "Payments";
                    this.c1FlexGridPmntTotal.DataSource = dsPayment;
                    this.c1FlexGridPmntTotal.Visible = false;
                }

                #endregion
                tbpgPatPmt.Tag = "isPatPmtLoaded";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                tbpgPatPmt.Tag = "";
            }
            finally
            {

                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        }

        private void FillGlobalPeriods()
        {
            gloBilling.clsGlobalPeriods objGlobalPeriods = new gloBilling.clsGlobalPeriods();

            try
            {
                DataTable _dtGlobalPeriodsList = objGlobalPeriods.GetGlobalPeriodsList(_nPAccountId);
                if (_dtGlobalPeriodsList != null)
                {
                    c1GlobalPeriods.DataSource = _dtGlobalPeriodsList;

                    c1GlobalPeriods.Cols["ID"].Visible = false;
                    c1GlobalPeriods.Cols["Patient Name"].Width = 120;
                    c1GlobalPeriods.Cols["Insurance"].Width = 300;
                    c1GlobalPeriods.Cols["Provider"].Width = 200;
                    c1GlobalPeriods.Cols["Dates"].Width = 200;
                    c1GlobalPeriods.AllowEditing = false;
                    c1GlobalPeriods.AllowDragging = AllowDraggingEnum.None;
                    c1GlobalPeriods.ExtendLastCol = true;
                    tbpgGlobalPeriods.Tag = "isGlobalPeriodsLoaded";
                    c1GlobalPeriods.Cols["StartDate"].Visible = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                tbpgGlobalPeriods.Tag = "";
            }
            finally
            {
                if (objGlobalPeriods != null) { objGlobalPeriods.Dispose(); }
            }

        }

        private void FillPatientReserves()
        {

            //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);
            }
            DataSet dsPayment = new DataSet();
            DataSet dsReserves = new DataSet();
            DataTable dtPatientRefund = new DataTable();

            try
            {

                #region "Reserves"
                objPatFinacialView.GetPatientReserve(out dsReserves);

                this.c1FlexGridAvailResrv.DataMember = "Reserves";
                this.c1FlexGridAvailResrv.DataSource = dsReserves;
                c1FlexGridAvailResrv.ShowCellLabels = false;
                if (isBadDebtPatient)
                {
                    c1FlexGridAvailResrv.Cols["Collection Agency"].Visible = true;
                }
                else
                {
                    c1FlexGridAvailResrv.Cols["Collection Agency"].Visible = false;
                }
                if (dsReserves.Tables["Reserves"].Rows.Count > 0)
                {
                    //tsb_PatRefund.Enabled = true;
                    this.c1FlexGridTotalAvailResrv.Visible = true;
                    this.c1FlexGridTotalAvailResrv.Rows[0].Visible = false;

                    this.c1FlexGridTotalAvailResrv.DataMember = "TotalReserves";
                    this.c1FlexGridTotalAvailResrv.DataSource = dsReserves;
                    setGridStyle(c1FlexGridTotalAvailResrv, c1FlexGridTotalAvailResrv.Rows.Count - 1, 11, 25);
                    if (dsReserves.Tables["Reserves"].Rows.Count >= iReserveSelRow)
                        c1FlexGridAvailResrv.Row = iReserveSelRow;
                }
                else
                {
                    this.c1FlexGridTotalAvailResrv.DataMember = "Reserves";
                    this.c1FlexGridTotalAvailResrv.DataSource = dsReserves;
                    this.c1FlexGridTotalAvailResrv.Visible = false;
                }
                #endregion
                tbpgPatPmt.Tag = "isPatPmtLoaded";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                tbpgPatPmt.Tag = "";
            }
            finally
            {
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        }

        private void FillPatientRefunds()
        {
            object _TotalRefund = null;
            decimal _totalrefund = 0;
            //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);
            }
            DataTable dtPatientRefund = new DataTable();

            try
            {
                #region "Refund"

                dtPatientRefund = objPatFinacialView.FillPatRefund();
                //if (dtPatientRefund.Rows.Count > 0)
                //{
                //    tsb_ViewPatRefund.Enabled = true;
                //}
                //Sum of the Refund amount.
                _TotalRefund = dtPatientRefund.Compute("SUM(nRefundAmount)", "Status <> 'Voided' AND Isnull(nRefundAmount,0) <> 0");
                if (_TotalRefund != null && _TotalRefund.ToString() != "")
                    _totalrefund = Convert.ToDecimal(_TotalRefund);
                c1PatientRefund.Cols.Add();
                c1PatientRefund.DataSource = dtPatientRefund.DefaultView;
                DesignPatientRefundgrid(_totalrefund);
                setGridStyle(c1RefundTotal, c1RefundTotal.Rows.Count - 1, 1, 6);
                #endregion

                tbpgPatPmt.Tag = "isPatPmtLoaded";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                tbpgPatPmt.Tag = "";
            }
            finally
            {
                if (_TotalRefund != null)
                    _TotalRefund = null;
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        }

        private void FillClaim_Charges()
        {


            DataView _dv = null;
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);
            }
            DataSet dsClaims_Charges = new DataSet();

            try
            {

                objPatFinacialView.GetClaimsNCharges(out dsClaims_Charges);
                if (dsClaims_Charges.Tables["Claims_Charges"] != null)
                {
                    gloGlobal.gloPMGlobal.SplitClaimColumn(dsClaims_Charges.Tables["Claims_Charges"], dsClaims_Charges.Tables["Claims_Charges"].Columns.IndexOf("SplitClaimNumber"));
                    if (dsClaims_Charges.Tables["Claims_Charges"].Rows.Count > 0)
                    {
                        _dv = dsClaims_Charges.Tables["Claims_Charges"].DefaultView;
                        if (tsb_ShowZeroBalance.Visible == true)
                        {
                            _dv.RowFilter = "TotalBalanceAmount <> 0";
                        }
                        else
                        {
                            _dv.RowFilter = "isnull(Party,'0') <> 'V'";
                        }

                        if (this.rbtn_Claim.Checked)
                        {
                            this.rbtn_Claim.Font = this._fBold;
                            _dv.Sort = "SortClaim Desc,SortSubClaim ASC, nTransactionLineNo";
                            c1FlexGridChargesClaims.DataSource = _dv;
                            CalculateTotalBalances(_dv);
                        }
                        else
                        {
                            this.rbtn_Claim.Font = this._fRegular;
                            _dv.Sort = " nDOS DESC,SortClaim Desc,SortSubClaim ASC,nTransactionLineNo ";
                            c1FlexGridChargesClaims.DataSource = _dv;
                            CalculateTotalBalances(_dv);
                        }
                        SetNoteFlag();

                        c1FlexGridChargesClaims.Refresh();
                        if (_dv.Count > 0)
                        {
                            tsbViewHistory.Visible = true;
                        }
                        else
                        {
                            tsbViewHistory.Visible = false;
                        }
                        tsbViewHistory.Tag = "Show";

                        //for (cntr = 0; cntr < (c1FlexGridChargesClaims.Rows.Count - 1); cntr++)
                        //{
                        //    if (Convert.ToBoolean(Convert.ToString(c1FlexGridChargesClaims.GetData(cntr + 1, c1FlexGridChargesClaims.Cols["blnNoteFlag"].Index)) == "" ? 0 : c1FlexGridChargesClaims.GetData(cntr + 1, c1FlexGridChargesClaims.Cols["blnNoteFlag"].Index)))
                        //    {
                        //        System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Notes;
                        //        this.c1FlexGridChargesClaims.SetCellImage(cntr + 1, COL_NOTE_IMAGE, imgFlag);
                        //    }
                        //    if (c1FlexGridChargesClaims.Rows.Count >= iChargesSelRow)
                        //        c1FlexGridChargesClaims.Row = iChargesSelRow;
                        //    else if (dsClaims_Charges.Tables["Claims_Charges"].Rows.Count > 1)
                        //        c1FlexGridChargesClaims.Row = 1;
                        //}
                        FillClaimOnHold();
                        tsb_btnTransferResponsibility.Enabled = true;
                    }
                    else
                    {
                        _dv = dsClaims_Charges.Tables["Claims_Charges"].DefaultView;
                        tsbViewHistory.Visible = false;
                        tsbViewHistory.Tag = "Hide";
                        this.c1FlexGridChargesClaims.DataSource = _dv;
                        this.c1FlexGridTotalChargesClaims.DataSource = _dv;
                        this.c1FlexGridTotalChargesClaims.Rows[0].Visible = false;
                        tsb_btnTransferResponsibility.Enabled = false;

                    }
                }
                tbpgCharge_Claims.Tag = "isClaimNChargesLoaded";
                // SetNoteFlag();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                this.c1FlexGridTotalChargesClaims.Rows[0].Visible = false;
                tbpgCharge_Claims.Tag = "";
            }
            finally
            {
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }

        }

        private void showHideZeroBalances()
        {
            DataView _dv = null;
            try
            {
                _dv = (DataView)c1FlexGridChargesClaims.DataSource;
                if (_dv != null)
                {
                    if (tsb_ShowZeroBalance.Visible == true)
                    {
                        _dv.RowFilter = "TotalBalanceAmount <> 0";
                        c1FlexGridChargesClaims.DataSource = _dv;
                        c1FlexGridChargesClaims.Refresh();
                        if (_dv.Count > 0)
                        {
                            tsbViewHistory.Visible = true;
                        }
                        else
                        {
                            tsbViewHistory.Visible = false;
                        }

                        CalculateTotalBalances(_dv);
                        SetNoteFlag();
                        FillClaimOnHold();

                    }
                    else
                    {
                        _dv.RowFilter = "";
                        _dv.RowFilter = "isnull(Party,'0') <> 'V'";
                        c1FlexGridChargesClaims.DataSource = _dv;
                        c1FlexGridChargesClaims.Refresh();
                        if (_dv.Count > 0)
                        {
                            tsbViewHistory.Visible = true;
                        }
                        else
                        {
                            tsbViewHistory.Visible = false;
                        }

                        CalculateTotalBalances(_dv);
                        SetNoteFlag();
                        FillClaimOnHold();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void CalculateTotalBalances(DataView dvClaimCharges)
        {
             DataTable dtClaimsNCharges = dvClaimCharges.ToTable();
             DataTable dtTotalClaimsNCharges = new DataTable();
            try
            {
                if (dvClaimCharges != null && dvClaimCharges.ToTable().Rows.Count > 0)
                {
                   
                    decimal decAmount = 0;
                    decimal decAdjs = 0;
                    decimal decInsPmnt = 0;
                    decimal decPatPmnt = 0;
                    decimal decInsPending = 0;
                    decimal decPatPending = 0;
                    decimal decBadDebtPending = 0;
                     

                    if (dtClaimsNCharges.Rows.Count > 0)
                    {

                        dtClaimsNCharges.CaseSensitive = false;
                        //Bug #86192: 00000934 : Claim on hold alert for voided claims
                        Decimal.TryParse(dtClaimsNCharges.Compute("Sum(dTotal)", " Party <> 'v' ").ToString(),out decAmount);
                        Decimal.TryParse(dtClaimsNCharges.Compute("Sum(PreviousAdjustment)", " Party <> 'v' ").ToString(), out decAdjs);
                        Decimal.TryParse(dtClaimsNCharges.Compute("Sum(InsurancePayment)", " Party <> 'v' ").ToString(), out decInsPmnt);
                        Decimal.TryParse(dtClaimsNCharges.Compute("Sum(PatientPayment)", " Party <> 'v' ").ToString(), out decPatPmnt);
                        Decimal.TryParse(dtClaimsNCharges.Compute("Sum(InsurancePending)", " Party <> 'v' ").ToString(), out decInsPending);
                        Decimal.TryParse(dtClaimsNCharges.Compute("Sum(PatientDue)", " Party <> 'v' ").ToString(), out decPatPending);
                        Decimal.TryParse(dtClaimsNCharges.Compute("Sum(BadDebtDue)", " Party <> 'v' ").ToString(), out decBadDebtPending);
                        dtTotalClaimsNCharges = dtClaimsNCharges.Clone();
                        dtTotalClaimsNCharges.ImportRow(dtClaimsNCharges.Rows[dtClaimsNCharges.Rows.Count - 1]);
                        dtTotalClaimsNCharges.Rows[0]["dTotal"] = decAmount;
                        dtTotalClaimsNCharges.Rows[0]["PreviousAdjustment"] = decAdjs;
                        dtTotalClaimsNCharges.Rows[0]["InsurancePayment"] = decInsPmnt;
                        dtTotalClaimsNCharges.Rows[0]["PatientPayment"] = decPatPmnt;
                        dtTotalClaimsNCharges.Rows[0]["InsurancePending"] = decInsPending;
                        dtTotalClaimsNCharges.Rows[0]["PatientDue"] = decPatPending;
                        dtTotalClaimsNCharges.Rows[0]["BadDebtDue"] = decBadDebtPending;
                        dtTotalClaimsNCharges.Rows[0]["SplitClaimNumber"] = "Total :";
                        dtTotalClaimsNCharges.Rows[0]["DOS"] = DBNull.Value;
                        dtTotalClaimsNCharges.Rows[0]["sCPTCode"] = "";
                        dtTotalClaimsNCharges.Rows[0]["sMod1Code"] = "";
                        dtTotalClaimsNCharges.Rows[0]["sMod2Code"] = "";
                        dtTotalClaimsNCharges.Rows[0]["sDx1Code"] = "";
                        dtTotalClaimsNCharges.Rows[0]["sDx2Code"] = "";
                        dtTotalClaimsNCharges.Rows[0]["sDx3Code"] = "";
                        dtTotalClaimsNCharges.Rows[0]["sDx4Code"] = "";
                        dtTotalClaimsNCharges.Rows[0]["nProviderName"] = "";
                        dtTotalClaimsNCharges.Rows[0]["nCloseDate"] = DBNull.Value;
                        dtTotalClaimsNCharges.Rows[0]["Party"] = "";
                        dtTotalClaimsNCharges.Rows[0]["sPatientName"] = "";

                        dtTotalClaimsNCharges.AcceptChanges();
                        this.c1FlexGridTotalChargesClaims.Rows[0].Visible = false;
                        //c1FlexGridTotalChargesClaims.Clear();
                        c1FlexGridTotalChargesClaims.DataSource = null;
                        c1FlexGridTotalChargesClaims.DataSource = dtTotalClaimsNCharges;
                        
                        setGridStyle(c1FlexGridTotalChargesClaims, c1FlexGridTotalChargesClaims.Rows.Count - 1, c1FlexGridTotalChargesClaims.Cols["SplitClaimNumber"].Index , c1FlexGridTotalChargesClaims.Cols.Count - 1);

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void FillStatement()
        {
            //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);
            }

            DataSet dsStatement = new DataSet();
            bool _IsPatientExcluded = false;

            try
            {
                objPatFinacialView.Fill_PatientStatement(out dsStatement);
                dsStatement.Tables[0].TableName = "Statement";
                this.c1FlexGrid_Statements.DataMember = "Statement";
                this.c1FlexGrid_Statements.DataSource = dsStatement;

                DataTable dt = dsStatement.Tables[0];
               
                c1FlexGrid_Statements.Cols[11].Visible = false;
                c1FlexGrid_Statements.Cols[13].Name = "Note";
                c1FlexGrid_Statements.Cols[13].Caption = "";
                c1FlexGrid_Statements.Cols[13].Width = 18;
                c1FlexGrid_Statements.Cols[13].Visible = true;
                c1FlexGrid_Statements.Cols[13].DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                c1FlexGrid_Statements.Cols[13].ComboList = "...";
                c1FlexGrid_Statements.Cols[13].AllowEditing = true;
                c1FlexGrid_Statements.Cols[13].ImageAlign = ImageAlignEnum.CenterCenter;
                c1FlexGrid_Statements.Cols[14].Visible = false;
                if (c1FlexGrid_Statements != null && c1FlexGrid_Statements.Cols.Count > 0)
                {
                    for (int colInd = 0; colInd < c1FlexGrid_Statements.Cols.Count; colInd++)
                    {
                        if (c1FlexGrid_Statements.Cols[colInd].Name != "Note")
                        {
                            c1FlexGrid_Statements.Cols[colInd].AllowEditing = false;
                        }
                    }
                }
                if (c1FlexGrid_Statements != null && c1FlexGrid_Statements.Rows.Count > 0)
                {
                    for (int RowInd = 1; RowInd < c1FlexGrid_Statements.Rows.Count; RowInd++)
                    {
                        if (Convert.ToString(c1FlexGrid_Statements.Rows[RowInd]["Status"].ToString()) != "Voided")
                        {
                            c1FlexGrid_Statements.Rows[RowInd].AllowEditing = false;
                        }
                    }
                }

                _IsPatientExcluded = objPatFinacialView.IsPatientExclude();

                if (_IsPatientExcluded)
                {
                    lblPatExcluded.Visible = true;
                    //Indicates Account Selected with out any specific patient means "All Acct Patients"
                    if (_nSelectedPatientId == 0)
                    {
                        lblPatExcluded.Text = "Account excluded from statements";
                    }
                    else
                    {
                        lblPatExcluded.Text = "Patient excluded from statements";
                    }
                }
                else
                {
                    lblPatExcluded.Visible = false;
                }
                tbpgStatements.Tag = "isStatementLoaded";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                tbpgStatements.Tag = "";
            }
            finally
            {
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        }

        private void FillSummary()
        {
            //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);

            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nSelectedPatientId, _nPAccountId);
            }

            DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            DataSet dsSummary = new DataSet();
            try
            {
                if (this.rbtn_Responsibility.Checked)
                {
                    _AgingSortByInSummary = 'R';
                }
                else if (this.rbtn_DOS_Summary.Checked)
                {
                    _AgingSortByInSummary = 'S';
                }

                objPatFinacialView.GetSummary(this._AgingSortByInSummary, out dsSummary);

                this.c1FlexGridSummary.DataMember = "Summary";
                this.c1FlexGridSummary.DataSource = dsSummary;

                c1FlexGridSummary.Cols["dtLastRemitteded"].Format = "MM/dd/yyyy";

                //Code added by SaiKrishna for Account Feature regarding rowsytle
                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle;// = c1FlexGridSummary.Styles.Add("cs_SummaryRowStyle");
                try
                {
                    if (c1FlexGridSummary.Styles.Contains("cs_SummaryRowStyle"))
                    {
                        csClaimRowStyle = c1FlexGridSummary.Styles["cs_SummaryRowStyle"];
                    }
                    else
                    {
                        csClaimRowStyle = c1FlexGridSummary.Styles.Add("cs_SummaryRowStyle");
                        csClaimRowStyle.DataType = typeof(System.String);
                        csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }

                }
                catch
                {
                    csClaimRowStyle = c1FlexGridSummary.Styles.Add("cs_SummaryRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                }
   
  

                if (c1FlexGridSummary.Rows.Count > 1)
                {
                    for (int intCnt = 0; intCnt < c1FlexGridSummary.Rows.Count - 1; intCnt++)
                    {
                        if (c1FlexGridSummary.Rows[intCnt]["ResponsibleParty"].ToString() == "")
                        {
                            c1FlexGridSummary.Rows[intCnt].Style = c1FlexGridSummary.Styles["cs_SummaryRowStyle"];
                        }
                        if (c1FlexGridSummary.Rows[intCnt]["ResponsibleParty"].ToString() == "Total :")
                        {
                            setGridStyle(c1FlexGridSummary, intCnt, 1, 8);
                            c1FlexGridSummary.Row = intCnt;
                        }
                    }

                    setGridStyle(c1FlexGridSummary, c1FlexGridSummary.Rows.Count - 1, 1, 8);
                    c1FlexGridSummary.Row = c1FlexGridSummary.Rows.Count - 1;
                    tbPgSummary.Tag = "isSummaryLoaded";
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                tbPgSummary.Tag = "";
            }
            finally
            {

                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }

        }

        private void GenerateReport()
        {
            string strReportServer = string.Empty;
            string strReportFolder = string.Empty;
            string strVirtualDir = string.Empty;
            System.Uri SSRSReportURL;
            //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID);

            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(this._nPatientID);
            }

            //RETRIVING REPORT SERVER NAME
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
            object oValue = new object();
            oSetting.GetSetting("ReportServer", out oValue);
            if (oValue != null)
            {
                strReportServer = oValue.ToString();
                oValue = null;
            }


            oSetting.GetSetting("ReportProtocol", out oValue);
            if (oValue != null)
            {
                strReportProtocol = oValue.ToString();
                oValue = null;
            }


            //RETRIVING REPORT FOLDER NAME
            oSetting.GetSetting("ReportFolder", out oValue);
            if (oValue != null)
            {
                strReportFolder = oValue.ToString();
                oValue = null;
            }

            //RETRIVING VIRTUAL DIRECTORY NAME
            oSetting.GetSetting("ReportVirtualDirectory", out oValue);
            if (oValue != null)
            {
                strVirtualDir = oValue.ToString();
                oValue = null;

            }



            if (strReportProtocol == "" || strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
            {
                MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SSRSReportURL = new Uri(strReportProtocol + "://" + strReportServer + "/" + strVirtualDir);
                PAHreportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                PAHreportViewer.ServerReport.ReportServerUrl = SSRSReportURL;
                PAHreportViewer.PageCountMode = PageCountMode.Actual;

            }
            catch (Exception ex)
            {
                MessageBox.Show("SSRS Reporting Service is not available.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return;
            }

            string reportParam = "&suser=" + gloPMGlobal.UserName + "&Practice=" + objPatFinacialView.getClinicName();// +"&Conn=" +  gloPMGlobal.DatabaseConnectionString;
            //this.Text = _reportTitle;

            List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();

            if (rbClaim.Checked == true)
            {
                PAHreportViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + "rptPatientAccountHistory";// +reportParam;
                _reportName = "rptPatientAccountHistory";
            }
            else if (rbPayment.Checked == true)
            {
                PAHreportViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + "rptPatientAccountHistory_Check";// +reportParam;
                _reportName = "rptPatientAccountHistory_Check";
            }
           
            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", gloPMGlobal.UserName, false));
            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", objPatFinacialView.getClinicName(), false));

            if (oPatientControl.IsAllAccPatSelected && rbClaim.Checked == true)
            {
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("nPatientID","0", false));
               
            }
            else
            {
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("nPatientID", this._nPatientID.ToString(), false));
               
            }


           // paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("nPatientID", _nPatientID.ToString(), false));
            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("nPAccountID", _nPAccountId.ToString(), false));

            //paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Conn",  gloPMGlobal.DatabaseConnectionString, false));
            this.PAHreportViewer.ServerReport.SetParameters(paramList);
            this.PAHreportViewer.RefreshReport();
            if (rbClaim.Checked == true)
            {
                tbpgReport.Tag = "isReportLoaded";
            }
            else if (rbPayment.Checked == true)
            {
                tbpgReport.Tag = "isPaymentReportLoaded";
            }

            _parameterName = String.Empty;
            _ParameterValue = String.Empty;
            foreach (ReportParameter param in paramList)
            {
                if (_parameterName == String.Empty)
                {
                    _parameterName = param.Name;
                }
                else
                {
                    _parameterName += "," + param.Name;
                }
                if (_ParameterValue == String.Empty)
                {
                    _ParameterValue = param.Values[0];
                }
                else
                {
                    _ParameterValue += "," + param.Values[0];
                }
            }
            
        }

        private void ApplyFilterToAccountLogGrid()
        {
            c1PALogView.AllowFiltering = true;
            filter = new AccountLogTypeFilter();
            c1PALogView.Cols[(int)GridColumnAccountLog.Type].Filter = filter;

            C1.Win.C1FlexGrid.CellStyle cs;// = this.c1PALogView.Styles.Add("HeaderFilterIcon");
            try
            {
                if (this.c1PALogView.Styles.Contains("HeaderFilterIcon"))
                {
                    cs = this.c1PALogView.Styles["HeaderFilterIcon"];
                }
                else
                {
                    cs = this.c1PALogView.Styles.Add("HeaderFilterIcon");
                    //Capture BackgroundImage of FilterEditor and assign it to the
                    //cs.BackgroundImage = this.c1PALogView.Glyphs(C1.Win.C1FlexGrid.GlyphEnum.FilterEditor);
                    cs.BackgroundImage = Resources.filter;
                    cs.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.RightCenter;
                }

            }
            catch
            {
                cs = this.c1PALogView.Styles.Add("HeaderFilterIcon");
                //Capture BackgroundImage of FilterEditor and assign it to the
                //cs.BackgroundImage = this.c1PALogView.Glyphs(C1.Win.C1FlexGrid.GlyphEnum.FilterEditor);
                cs.BackgroundImage = Resources.filter;
                cs.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.RightCenter;
            }
          

            C1.Win.C1FlexGrid.CellStyle csRemove;// = this.c1PALogView.Styles.Add("RemoveHeaderFilterIcon");
            try
            {
                if (this.c1PALogView.Styles.Contains("RemoveHeaderFilterIcon"))
                {
                    csRemove = this.c1PALogView.Styles["RemoveHeaderFilterIcon"];
                }
                else
                {
                    csRemove = this.c1PALogView.Styles.Add("RemoveHeaderFilterIcon");
                    //Capture BackgroundImage of FilterEditor and assign it to the
                    //cs.BackgroundImage = this.c1PALogView.Glyphs(C1.Win.C1FlexGrid.GlyphEnum.FilterEditor);
                    csRemove.BackgroundImage = null;
                    csRemove.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.RightCenter;
                }

            }
            catch
            {
                csRemove = this.c1PALogView.Styles.Add("RemoveHeaderFilterIcon");
                //Capture BackgroundImage of FilterEditor and assign it to the
                //cs.BackgroundImage = this.c1PALogView.Glyphs(C1.Win.C1FlexGrid.GlyphEnum.FilterEditor);
                csRemove.BackgroundImage = null;
                csRemove.BackgroundImageLayout = C1.Win.C1FlexGrid.ImageAlignEnum.RightCenter;

            }
          

            c1PALogView.SetCellStyle(0, (int)GridColumnAccountLog.Type, cs);
        }

        private void OpenReserveForModify(int RowIndex)
        {
            Int64 nEobPaymentId = 0;
            Int64 nPatientId = 0;
            Int64 nEobPayDetailId = 0;
            Decimal _reserveAmount = 0;
            try
            {
                if (c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentID"].Index)).Trim() != "")
                { nEobPaymentId = Convert.ToInt64(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentID"].Index)); }

                if (c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nResEOBPaymentID"].Index) != null && Convert.ToString(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nResEOBPaymentID"].Index)).Trim() != "")
                { nEobPayDetailId = Convert.ToInt64(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nResEOBPaymentID"].Index)); }

                //if (c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nReserveID"].Index) != null && Convert.ToString(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nReserveID"].Index)).Trim() != "")
                //{ nEobPayDetailId = Convert.ToInt64(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nReserveID"].Index)); }

                if (c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nPatientID"].Index) != null && Convert.ToString(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nPatientID"].Index)).Trim() != "")
                { nPatientId = Convert.ToInt64(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nPatientID"].Index)); }

                if (c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["AvailableReserve"].Index) != null && Convert.ToString(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["AvailableReserve"].Index)).Trim() != "")
                { _reserveAmount = Convert.ToDecimal(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["AvailableReserve"].Index)); }

                frmPaymentReserveRemaningV2 ofrmPaymentReserveRemaning = new frmPaymentReserveRemaningV2(gloPMGlobal.DatabaseConnectionString, nPatientId, nEobPaymentId, nEobPayDetailId, true);
                //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
                ofrmPaymentReserveRemaning.PAccountID = _nPAccountId;
                ofrmPaymentReserveRemaning.IsProviderMandatory = IsProviderEnable;
                ofrmPaymentReserveRemaning.ReserveAmountFromUR = _reserveAmount;
                ofrmPaymentReserveRemaning.ShowDialog(this);
                if (ofrmPaymentReserveRemaning.DialogResult == DialogResult.OK)
                {
                    oPatientControl.RefreshBalances();
                    FillPatientReserves();
                }
                ofrmPaymentReserveRemaning.Dispose();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            { }
        }


        private void FillClaimOnHold()
        {
            gloOffice.gloC1FlexStyle.Style(c1FlexGridChargesClaims, false);
             DataTable dtClaimOnHold = new DataTable();
             bool bIsClaimsHold = false;
            try
            {
                
                if (oPatientControl.IsAllAccPatSelected)
                {
                    dtClaimOnHold = gloStripControl.PatientStripControl.GetClaimsOnHold(0, _nPAccountId);

                }
                else
                {
                   
                    dtClaimOnHold = gloStripControl.PatientStripControl.GetClaimsOnHold(_nPatientID, _nPAccountId);
                   
                    
                }

                

                if (dtClaimOnHold.Rows.Count > 0)
                {
                    strArrClaims = dtClaimOnHold.Rows[0]["ClaimNo"].ToString().Split(',');
                    bIsClaimsHold = true;
                }
                
                #region "Set Style"
                CellStyle csSubCol;
                //   csSubCol = c1FlexGridChargesClaims.Styles.Add("SubCol");
                try
                {
                    if (c1FlexGridChargesClaims.Styles.Contains("SubCol"))
                    {
                        csSubCol = c1FlexGridChargesClaims.Styles["SubCol"];
                    }
                    else
                    {
                        csSubCol = c1FlexGridChargesClaims.Styles.Add("SubCol");
                    }
                    if (!bIsClaimsHold)
                    {
                        c1FlexGridChargesClaims.Styles.Remove("SubCol");
                    }
                }
                catch
                {
                    csSubCol = c1FlexGridChargesClaims.Styles.Add("SubCol");


                }
                csSubCol.ForeColor = Color.Red;

                CellStyle csSubColTest;
                // csSubColTest = c1FlexGridChargesClaims.Styles.Add("csSubColTest");
                try
                {
                    if (c1FlexGridChargesClaims.Styles.Contains("csSubColTest"))
                    {
                        csSubColTest = c1FlexGridChargesClaims.Styles["csSubColTest"];
                    }
                    else
                    {
                        csSubColTest = c1FlexGridChargesClaims.Styles.Add("csSubColTest");
                    }
                    if (!bIsClaimsHold)
                    {
                        c1FlexGridChargesClaims.Styles.Remove("csSubColTest");
                    }
                }
                catch
                {
                    csSubColTest = c1FlexGridChargesClaims.Styles.Add("csSubColTest");


                }
                csSubColTest.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
                #endregion
                //c1FlexGridChargesClaims.Redraw = false;

                //// Checking the grid for hold claims
                //for (int iGridClaimCount = 1; iGridClaimCount <= c1FlexGridChargesClaims.Rows.Count - 1; iGridClaimCount++)
                //{
                //    // Check if the current claim exist in hold array
                //    if ((strArrClaims!=null) && (strArrClaims.Contains(c1FlexGridChargesClaims.GetData(iGridClaimCount, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString())))
                //    {
                //        // Format with red if claim is on hold
                       
                //        c1FlexGridChargesClaims.Rows[iGridClaimCount].Style = csSubCol;
                //    }
                //    else
                //    {
                //        // Reset default format if claim is not on hold
                        
                //        c1FlexGridChargesClaims.Rows[iGridClaimCount].Style = csSubColTest;
                //    }
                //}
                //c1FlexGridChargesClaims.Redraw = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (dtClaimOnHold != null)
                    dtClaimOnHold.Dispose();
            }
        }

        private void setNormalGridStyle(int rowNum)
        {
            CellStyle csSubCol1;
           // csSubCol1 = c1FlexGridChargesClaims.Styles.Add("SubCol1");
            try
            {
                if (c1FlexGridChargesClaims.Styles.Contains("SubCol1"))
                {
                    csSubCol1 = c1FlexGridChargesClaims.Styles["SubCol1"];
                }
                else
                {
                    csSubCol1 = c1FlexGridChargesClaims.Styles.Add("SubCol1");
                    csSubCol1.TextAlign = TextAlignEnum.LeftCenter;
                    csSubCol1.BackColor = Color.FromArgb(255, 255, 255);
                    csSubCol1.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubCol1.TextEffect = TextEffectEnum.Flat;
                    csSubCol1.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
                }

            }
            catch
            {
                csSubCol1 = c1FlexGridChargesClaims.Styles.Add("SubCol1");
                csSubCol1.TextAlign = TextAlignEnum.LeftCenter;
                csSubCol1.BackColor = Color.FromArgb(255, 255, 255);
                csSubCol1.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubCol1.TextEffect = TextEffectEnum.Flat;
                csSubCol1.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);


            }
  
            c1FlexGridChargesClaims.Rows[rowNum].Style = csSubCol1;
        }

        private void LoadInsuranceRefundLog()
        {
            DataTable _dtRefundLog = new DataTable();
            //gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            gloPatientFinancialViewV2 objPatFinacialView = null;
            if (oPatientControl.IsAllAccPatSelected)
            {
                objPatFinacialView = new gloPatientFinancialViewV2(0, _nPAccountId);
            }
            else
            {
                objPatFinacialView = new gloPatientFinancialViewV2(_nPatientID, _nPAccountId);
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                //DesignRefundGrid();
                DesignRefundGrid(c1InsuranceRefundLog);
                _dtRefundLog = objPatFinacialView.GetPatientPaymentRefundLog(_nPatientID, _nPAccountId, gloPMGlobal.ClinicID);
                if (_dtRefundLog != null && _dtRefundLog.Rows.Count > 0)
                {
                    decimal _dTotalRefundAmount = 0;
                    foreach (DataRow row in _dtRefundLog.Rows)
                    {
                        c1InsuranceRefundLog.Rows.Add();
                        int i = c1InsuranceRefundLog.Rows.Count - 1;
                        c1InsuranceRefundLog.SetData(i, COL_CLOSEDATE_REF, String.Format("{0:MM/dd/yyyy}", row["CloseDate"]));
                        c1InsuranceRefundLog.SetData(i, COL_TRAY_REF, row["Tray"]);
                        c1InsuranceRefundLog.SetData(i, COL_COMPANY_REF, row["Company"]);
                        c1InsuranceRefundLog.SetData(i, COL_CHECK_NO_REF, row["CheckNumber"]);
                        c1InsuranceRefundLog.SetData(i, COL_REFUND_DATE_REF, String.Format("{0:MM/dd/yyyy}", row["PaymentDate"]));
                        c1InsuranceRefundLog.SetData(i, COL_REFUND_AMOUNT_REF, row["Amount"]);
                        c1InsuranceRefundLog.SetData(i, COL_NOTE_REF, row["sNoteDescription"]);
                        c1InsuranceRefundLog.SetData(i, COL_USER_REF, row["User Name"]);
                        c1InsuranceRefundLog.SetData(i, COL_USER_ID_REF, row["nUserID"]);
                        c1InsuranceRefundLog.SetData(i, COL_EOBPAYMENT_ID_REF, row["nEOBPaymentID"]);
                        c1InsuranceRefundLog.SetData(i, COL_STATUS_REF, row["Status"]);
                        c1InsuranceRefundLog.SetData(i, COL_DATETIME_REF, row["RefundDateTime"]);
                        c1InsuranceRefundLog.SetData(i, COL_REFUNDID_REF, row["nRefundId"]);
                        c1InsuranceRefundLog.SetData(i, COL_COMPANY_ID_REF, row["nPayerID"]);
                        c1InsuranceRefundLog.SetData(i, COL_CLAIMNO_REF, row["Claim"]);
                        //c1InsuranceRefundLog.SetData(i, COL_PATIENTNAME_REF, row["sRefundTo"]);
                        _dTotalRefundAmount = _dTotalRefundAmount + Convert.ToDecimal(row["Amount"]);
                    }
                    //DesignTotalRefundGrid();
                    DesignRefundGrid(c1InsRefundTotal);
                    c1InsRefundTotal.SetData(0, COL_CLOSEDATE_REF, "Total :");
                    c1InsRefundTotal.SetData(0, COL_REFUND_AMOUNT_REF, "$" + _dTotalRefundAmount);
                    c1InsRefundTotal.Width = c1InsuranceRefundLog.Width;
                    setGridStyle(c1InsRefundTotal, 0, COL_CLOSEDATE_REF, c1InsRefundTotal.Cols.Count - 1);
                    tbpgInsResAndRefund.Tag = "isInsResNRefLoaded";
                }
                else
                {
                    c1InsRefundTotal.Rows.Count = 0;
                }



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                tbpgInsResAndRefund.Tag = "";
            }
            finally
            {
                this.Cursor = Cursors.Default;
                _dtRefundLog.Dispose();
            }
        }

        private void ViewPatientStatementTemplate()
        {
            DataTable dttemp = new DataTable();                   
            gloOffice.gloTemplate ogloTemplate = new gloTemplate(gloPMGlobal.DatabaseConnectionString);
            dttemp = ogloTemplate.GetPatientTemplate(_nTrasactionID);

            if (dttemp != null && dttemp.Rows.Count > 0)
            {
                //nPatientID, nTemplateID , sTemplateName , nFromDate, nToDate, nProviderID, iTemplate, nCount, nClinicID
                if (dttemp.Rows[0]["iTemplate"] != null && Convert.ToString(dttemp.Rows[0]["iTemplate"].ToString()) != string.Empty)
                {
                    frmWd_PatientTemplate frm = new frmWd_PatientTemplate(gloPMGlobal.DatabaseConnectionString, _nTrasactionID, true);    
                    frm.MdiParent = this.ParentForm;
                    frm.IsView = true;
                    frm.Show();
                    frm.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    MessageBox.Show("Record cannot be opened as there are problems with the contents.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Close();
                }
            }
            
        }

        //Code changed by SaiKrishan.
        //private void Fill_PatientStatement(out DataSet dsPatFinView)
        //{
        //    SqlConnection _con = new SqlConnection(this. gloPMGlobal.DatabaseConnectionString);
        //    dsPatFinView = new DataSet();
        //    try
        //    {
        //        //string sqlQuery = "";

        //        //sqlQuery = "SELECT CASE BL_Batch_PatientStatement_Mst.bIsUnclosedDay  WHEN 1 THEN CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) + ' [Unclosed] '  ELSE CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) END AS dtStatementDate,Convert(varchar,BL_Batch_PatientStatement_Mst.dtCreateDate,101) As dtCreateDate,ISNULL(BL_Batch_PatientStatement_Mst.sBatchName,'') as sBatchName, " +
        //        //          "ISNULL(BL_Batch_PatientStatement_Mst.sUserName,'') as sUserName, ISNULL(Patient.sFirstName,'') as sFirstName, ISNULL(Patient.sMiddleName,'') as sMiddleName, ISNULL(Patient.sLastName,'') as sLastName, " +
        //        //          "ISNULL(BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID,0) as nBatchPateintStatMstID,ISNULL(BL_Batch_PatientStatement_DTL.nTempleteTransactionID,0) as nTempleteTransactionID, ISNULL(Patient.nPatientID,0) as nPatientID,sVoidNotes As Notes,CASE WHEN isnull(BL_Batch_PatientStatement_DTL.bIsVoid,0) = 0 then ' ' ELSE 'Voided' END As Status,BL_Batch_PatientStatement_DTL.nBatchPateintStatDtlID" +
        //        //          " FROM Patient INNER JOIN " +
        //        //          "BL_Batch_PatientStatement_DTL ON Patient.nPatientID = BL_Batch_PatientStatement_DTL.nPatientID INNER JOIN " +
        //        //          " BL_Batch_PatientStatement_Mst ON BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID = BL_Batch_PatientStatement_Mst.nBatchPateintStatMstID where Patient.nPatientID=" + _nPatientID + " order by dtCreateDate desc";


        //        //Code  added by SaiKrishna
        //        //sqlQuery = "SELECT CASE BL_Batch_PatientStatement_Mst.bIsUnclosedDay  WHEN 1 THEN CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) + ' [Unclosed] '  ELSE CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) END AS dtStatementDate,Convert(varchar,BL_Batch_PatientStatement_Mst.dtCreateDate,101) As dtCreateDate,ISNULL(BL_Batch_PatientStatement_Mst.sBatchName,'') as sBatchName, " +
        //        //             "ISNULL(BL_Batch_PatientStatement_Mst.sUserName,'') as sUserName,"  +
        //        //             "ISNULL(BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID,0) as nBatchPateintStatMstID,ISNULL(BL_Batch_PatientStatement_DTL.nTempleteTransactionID,0) as nTempleteTransactionID,ISNULL(BL_Batch_PatientStatement_DTL.nVoidUserId,0) AS UserID,ISNULL(BL_Batch_PatientStatement_DTL.sVoidUserName,'') AS UserName,ISNULL(BL_Batch_PatientStatement_DTL.dtVoidDate,0) AS VoidDateTime,sVoidNotes As Notes,CASE WHEN isnull(BL_Batch_PatientStatement_DTL.bIsVoid,0) = 0 then ' ' ELSE 'Voided' END As Status,BL_Batch_PatientStatement_DTL.nBatchPateintStatDtlID" +
        //        //             " FROM PA_Accounts INNER JOIN " +
        //        //             "BL_Batch_PatientStatement_DTL ON PA_Accounts.nPAccountID = BL_Batch_PatientStatement_DTL.nPAccountID INNER JOIN " +
        //        //             " BL_Batch_PatientStatement_Mst ON BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID = BL_Batch_PatientStatement_Mst.nBatchPateintStatMstID where BL_Batch_PatientStatement_DTL.nPAccountID =" + _nPAccountId + " and BL_Batch_PatientStatement_DTL.nPatientID=" + _nSelectedPatientId + "  order by dtCreateDate desc";

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = _con;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "PA_Fill_PatientStatement";
        //        SqlParameter ParAccount = new SqlParameter();
        //        {
        //            ParAccount.ParameterName = "@nPAccountID";
        //            ParAccount.Value = _nPAccountId;
        //            ParAccount.Direction = ParameterDirection.Input;
        //            ParAccount.SqlDbType = SqlDbType.BigInt;
        //        }
        //        cmd.Parameters.Add(ParAccount);
        //        SqlParameter ParPatient = new SqlParameter();
        //        {
        //            ParPatient.ParameterName = "@nPatientID";
        //            ParPatient.Value = _nSelectedPatientId;
        //            ParPatient.Direction = ParameterDirection.Input;
        //            ParPatient.SqlDbType = SqlDbType.BigInt;
        //        }
        //        cmd.Parameters.Add(ParPatient);


        //        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //        adpt.Fill(dsPatFinView, "dtPatFinView");
        //        _con.Close();


        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //    finally
        //    {
        //        if (_con != null) { _con.Close(); }
        //    }

        //}

        //Code changed by SaiKrishan.ExcludeFromStament is related to account not patient


        private int GetExcludePatientDtl()
        {
            //SqlConnection _Con = new SqlConnection(this. gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            int _result;
            try
            {
                // _sqlQuery = " SELECT Count(*) FROM PatientSettings WHERE sValue = 1 AND sName = 'Exclude from Statement' AND nPatientID = " + _nPatientID + " ";
                _sqlQuery = " Select Count(nPAccountID) From PA_Accounts WITH (NOLOCK) Where bIsExcludeStatement =1 and  nPAccountID = " + _nPAccountId;
                oDB.Connect(false);
                _result = (int)oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();
                return _result;
            }
            catch
            {
                return 0;
            }
        }

        private void RemoveTabPageTag()
        {
            tbPgSummary.Tag = "";
            tbpgCharge_Claims.Tag = "";
            tbpgPatPmt.Tag = "";
            tbpgReport.Tag = "";
            tbpgStatements.Tag = "";
            tbpgInsResAndRefund.Tag = "";
            tbpgGlobalPeriods.Tag = "";
            tbpgAccountLog.Tag = "";
        }

        //Bug #77640: Patient Account. Check information is blank.
        public void getContactId(Int64 nCreditId, Int64 nTransId, Int64 nEOBId, out Int64 MstTransId, out Int64 ContactId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            DataTable _dtContactInfo = null;
            try
            {
                _sqlQuery = " Select top(1) isNull(nBillingTransactionID,0) as nBillingTransactionID,isNull(nContactID,0) as nContactID From EOB WITH (NOLOCK) Where nCreditID =" + nCreditId + " and nEOBID=" + nEOBId + " and nTrackTransactionID = " + nTransId;
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtContactInfo);
                if (_dtContactInfo != null && _dtContactInfo.Rows.Count > 0)
                {
                    MstTransId = Convert.ToInt64(_dtContactInfo.Rows[0]["nBillingTransactionID"]);
                    ContactId = Convert.ToInt64(_dtContactInfo.Rows[0]["nContactID"]);
                }
                else
                {
                    MstTransId = 0;
                    ContactId = 0;
                }
            }
            catch
            {
                MstTransId = 0;
                ContactId = 0;
            }
            finally
            {
                if (_dtContactInfo != null)
                {
                    _dtContactInfo.Dispose();
                    _dtContactInfo = null;
                }
                oDB.Disconnect();
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        private void ApplyDrillDown()
        {
            Int64 PatientID = 0;
            string SelectedActionType = string.Empty;
            try
            {
                if (c1PALogView.RowSel > 0)
                {
                    Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.PatientID)).Trim(), out PatientID);
                    if (c1PALogView.RowSel > 0) { SelectedActionType = Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.Type)).Trim(); }

                    switch (SelectedActionType)
                    {
                        case "InsPmt":
                        case "InsPmt Void":
                            if (c1PALogView.RowSel > 0)
                            {
                                Int64 EOBId = 0;
                                Int64 nCreditID = 0;
                                //Bug #77640: Patient Account. Check information is blank.
                                Int64 TransID = 0;
                                Int64 ContactID = 0;
                                Int64 MstTransId = 0;
                                Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.EOBID)).Trim(), out EOBId);
                                Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.CreditId)).Trim(), out nCreditID);
                                //Bug #77640: Patient Account. Check information is blank.
                                Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.TransactionID)), out TransID);

                                if (nCreditID > 0 && EOBId > 0)
                                {
                                    //Bug #77640: Patient Account. Check information is blank.
                                    getContactId(nCreditID, TransID, EOBId,out MstTransId,out ContactID);
                                    using (frmViewClaimRemittanceV2 ofrmViewClaimRemittance = new frmViewClaimRemittanceV2(AppSettings.ConnectionStringPM, 0, AppSettings.ClinicID, nCreditID, EOBId))
                                    {

                                        ofrmViewClaimRemittance.StartPosition = FormStartPosition.CenterScreen;
                                        ofrmViewClaimRemittance.CallingContainer = this.Name;
                                        //Bug #77640: Patient Account. Check information is blank.
                                        ofrmViewClaimRemittance.TransactionID = MstTransId;
                                        ofrmViewClaimRemittance.ContactID = ContactID;
                                        ofrmViewClaimRemittance.ShowDialog(this);
                                        ofrmViewClaimRemittance.Dispose();
                                        oPatientControl.RefreshBalances();

                                        //if (!worker.IsBusy)
                                        //{
                                        //    worker.RunWorkerAsync();
                                        //}
                                    }
                                }
                            }
                            break;
                        case "Claim":
                        case "Chrg Note":
                        case "Claim Note":
                        case "Clm Void":
                        case "Hold":
                        case "Release Hold":
                        case "Split":
                        case "Resp":
                        case "Bill":

                            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");
                            Int64 TransactionID = 0;
                            Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.TransactionID)), out TransactionID);
                            if (TransactionID > 0)
                            {
                                ogloBilling.ShowModifyCharges(PatientID, TransactionID, this.Name, this);
                             //   ogloBilling.Dispose();
                            }
                            if (ogloBilling != null)
                            {
                                ogloBilling.Dispose();
                                ogloBilling = null;
                            }
                            break;
                        case "PatPmt":
                        case "PatPmt Void":

                            Int64 CreditId = 0;
                            Int64 nMainCreditId = 0;
                            decimal Delta = 0;
                            object _retVal = null;
                            if (c1PALogView.RowSel > 0)
                            {
                                Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.CreditId)), out CreditId);
                                Decimal.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.Delta)), out Delta);
                            }
                            string _strQuery = " SELECT TOP 1 " +
                                               "         CASE " +
                                               "           WHEN ( SELECT   COUNT(nCreditID)" +
                                               "                FROM     dbo.Credits_DTL" +
                                               "                WHERE    nCreditID = " + CreditId +
                                               "              ) > 0 THEN nCreditsRef_ID " +
                                               "           ELSE 0 " +
                                               "         END AS CreditID " +
                                               " FROM    dbo.Credits_DTL " +
                                               " WHERE   nCreditID = " + CreditId + " AND dAmount = " + (Delta * -1);
                            if (CreditId > 0)
                            {
                                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                                oDB.Connect(false);
                                _retVal = oDB.ExecuteScalar_Query(_strQuery);
                                oDB.Disconnect();

                                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                                { nMainCreditId = Convert.ToInt64(_retVal); }

                                if (nMainCreditId == 0)
                                { nMainCreditId = CreditId; }
                                //nMainCreditId = Convert.ToInt64(oDB.ExecuteScalar_Query("select distinct nCredit_RefID from Debits WITH (NOLOCK) where nCreditID=" + CreditId + " and  nCredit_RefID <>0"));
                                gloAccountsV2.frmViewPatientPaymentV2 ofrmViewPatientPayment = new gloAccountsV2.frmViewPatientPaymentV2(gloPMGlobal.DatabaseConnectionString, PatientID, gloPMGlobal.ClinicID, nMainCreditId);
                                ofrmViewPatientPayment.StartPosition = FormStartPosition.CenterScreen;

                                oDB.Disconnect();
                                oDB.Dispose();

                                if (ofrmViewPatientPayment.IsVoidedNow)
                                {
                                    oPatientControl.DisableInputControl = true;
                                    pnlProgressIndication.BringToFront();
                                    oPatientControl.RefreshBalances();

                                    if (!worker.IsBusy)
                                    {
                                        worker.RunWorkerAsync();
                                    }
                                }
                                ofrmViewPatientPayment.ShowDialog(this);
                                ofrmViewPatientPayment.Dispose();
                            }

                            break;
                        case "Acct Note":

                            Int64 AccountNotesID = 0;
                            Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.NoteId)), out AccountNotesID);

                            gloPatient.frmAccountNotes ofrmAccountNotes = new gloPatient.frmAccountNotes();
                            ofrmAccountNotes.nPatientAccountID = oPatientControl.AccountPatientID;
                            ofrmAccountNotes.nPatientID = oPatientControl.PatientID;
                            ofrmAccountNotes.nPAccountID = oPatientControl.PAccountID;
                            ofrmAccountNotes.nHighlightNoteID = AccountNotesID;

                            ofrmAccountNotes.ShowDialog(this);
                            ofrmAccountNotes.Dispose();

                            break;
                        case "Patient Refund":
                        case "Patient Refund Void":

                            if (c1PALogView.Rows.Count > 0)
                            {
                                Int64 RefundID = 0;
                                Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.RefundID)), out RefundID);

                                if (RefundID > 0)
                                {
                                    frmPatientPayRefundViewV2 ofrmPatientPayRefundView = new frmPatientPayRefundViewV2(gloPMGlobal.DatabaseConnectionString, PatientID, RefundID);
                                    ofrmPatientPayRefundView.ShowDialog(this);
                                    ofrmPatientPayRefundView.Dispose();
                                    oPatientControl.RefreshBalances();
                                }
                            }

                            break;
                        case "Patient Alert":

                            if (c1PALogView.Rows.Count > 0)
                            {
                                Int64 PatAlertID = 0;
                                Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.NoteId)), out PatAlertID);

                                frmPatientAlerts ofrmPatientAlerts = new frmPatientAlerts(gloPMGlobal.DatabaseConnectionString, PatientID);
                                ofrmPatientAlerts.SelectedAlertID = PatAlertID;
                                ofrmPatientAlerts.ShowDialog(this);
                                ofrmPatientAlerts.Dispose();

                            }

                            break;

                        case "Stmt Note":

                            Int64 StmtNotesID = 0;
                            Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.NoteId)), out StmtNotesID);

                            if (oPatientControl.PatientID > 0)
                            {
                                gloBilling.frmPatientStatementNotes frmPSN = new gloBilling.frmPatientStatementNotes(gloPMGlobal.DatabaseConnectionString, PatientID);
                                try
                                {
                                    frmPSN.SelectedStmtNotesID = StmtNotesID;
                                    frmPSN.ShowDialog(this);
                                    frmPSN.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    ex = null;
                                }
                            }

                            break;

                        case "Pat Acct Stmt":

                            if (c1PALogView.Rows.Count > 0)
                            {
                                Int64.TryParse(Convert.ToString(c1PALogView.GetData(c1PALogView.RowSel, (int)GridColumnAccountLog.NoteId)), out _nTrasactionID);
                                ViewPatientStatementTemplate();

                            }

                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #region "Design Grid"

        private void DesignReserveGrid(C1FlexGrid c1ReserveGrid)
        {
            try
            {
                //_IsFormLoading = true;
                c1ReserveGrid.Redraw = false;
                c1ReserveGrid.AllowSorting = AllowSortingEnum.None;

                c1ReserveGrid.Clear();
                c1ReserveGrid.Cols.Count = COL_COUNT;
                c1ReserveGrid.Rows.Count = 1;
                c1ReserveGrid.Rows.Fixed = 1;
                c1ReserveGrid.Cols.Fixed = 0;

                #region " Set Headers "
                if (c1ReserveGrid.Name == "c1Reserve")
                {
                    c1ReserveGrid.SetData(0, COL_EOBPAYMENTID, "EOBPaymentID");
                    c1ReserveGrid.SetData(0, COL_SOURCE, "Source");//Patient or Insurance Name
                    c1ReserveGrid.SetData(0, COL_COMPANYNAME, "Insurance Company");
                    c1ReserveGrid.SetData(0, COL_ORIGINALPAYMENT, "Original Payment");//Check Number,Date,Amount         
                    c1ReserveGrid.SetData(0, COL_USERNAME, "User");
                    c1ReserveGrid.SetData(0, COL_TORESERVES, "To Reserves");//Amount for reserve
                    c1ReserveGrid.SetData(0, COL_TYPE, "Type");//Copay,Advance,Other
                    c1ReserveGrid.SetData(0, COL_NOTE, "Note");//Note
                    c1ReserveGrid.SetData(0, COL_AVAILABLE, "Available");//Available amount
                    c1ReserveGrid.SetData(0, COL_USERESERVE, "Used");//Used Reserve
                    c1ReserveGrid.SetData(0, COL_REFUND, "Refund");//Current amount to use from avaiable amount
                    c1ReserveGrid.SetData(0, COL_PAYMODE, "Payment Mode");
                    c1ReserveGrid.SetData(0, COL_PAYMENTCLOSEDATE, "Close Date");
                    c1ReserveGrid.SetData(0, COL_PAYMENTMODE, "sPaymentMode");
                    c1ReserveGrid.SetData(0, COL_PAYMENTMODENO, "sPaymentNo");
                    c1ReserveGrid.SetData(0, COL_ASSO_PATIENTID, "PatientID");
                    c1ReserveGrid.SetData(0, COL_ASSO_PATIENTNAME, "Patient");
                    c1ReserveGrid.SetData(0, COL_ASSO_MSTTRANSACTIONID, "nTransactionID");
                    c1ReserveGrid.SetData(0, COL_ASSO_TRACKTRANSACTIONID, "nTrackTrnID");
                    c1ReserveGrid.SetData(0, COL_ASSO_CLAIMNO, "Claim #");
                }
                #endregion

                #region " Show/Hide "

                c1ReserveGrid.Cols[COL_SOURCE].Visible = true;
                c1ReserveGrid.Cols[COL_ORIGINALPAYMENT].Visible = true;
                c1ReserveGrid.Cols[COL_TORESERVES].Visible = true;
                c1ReserveGrid.Cols[COL_EOBPAYMENTID].Visible = false;// 0;
                c1ReserveGrid.Cols[COL_COMPANYNAME].Visible = true;
                c1ReserveGrid.Cols[COL_USERNAME].Visible = true;// 100;
                c1ReserveGrid.Cols[COL_TYPE].Visible = false;// 100;
                c1ReserveGrid.Cols[COL_NOTE].Visible = true;// 100;
                c1ReserveGrid.Cols[COL_AVAILABLE].Visible = true;// 100;
                c1ReserveGrid.Cols[COL_REFUND].Visible = false;// 100;
                c1ReserveGrid.Cols[COL_USERESERVE].Visible = false;
                c1ReserveGrid.Cols[COL_PAYMODE].Visible = false;// 100;
                c1ReserveGrid.Cols[COL_PAYMENTCLOSEDATE].Visible = true;
                c1ReserveGrid.Cols[COL_PAYMENTMODE].Visible = false;
                c1ReserveGrid.Cols[COL_PAYMENTMODENO].Visible = false;
                c1ReserveGrid.Cols[COL_ASSO_PATIENTID].Visible = false;
                c1ReserveGrid.Cols[COL_ASSO_PATIENTNAME].Visible = false;
                c1ReserveGrid.Cols[COL_ASSO_MSTTRANSACTIONID].Visible = false;
                c1ReserveGrid.Cols[COL_ASSO_TRACKTRANSACTIONID].Visible = false;
                c1ReserveGrid.Cols[COL_ASSO_CLAIMNO].Visible = true;

                #endregion

                #region " Width "

                bool _designWidth = false;

                if (_designWidth == false)
                {

                    c1ReserveGrid.Cols[COL_EOBPAYMENTID].Width = 0;
                    c1ReserveGrid.Cols[COL_SOURCE].Width = 0;
                    c1ReserveGrid.Cols[COL_COMPANYNAME].Width = 140;
                    c1ReserveGrid.Cols[COL_ORIGINALPAYMENT].Width = 290;
                    c1ReserveGrid.Cols[COL_USERNAME].Width = 150;
                    c1ReserveGrid.Cols[COL_TORESERVES].Width = 100;
                    c1ReserveGrid.Cols[COL_TYPE].Width = 0;
                    c1ReserveGrid.Cols[COL_NOTE].Width = 255;
                    c1ReserveGrid.Cols[COL_AVAILABLE].Width = 100;
                    c1ReserveGrid.Cols[COL_REFUND].Width = 0;
                    c1ReserveGrid.Cols[COL_PAYMODE].Width = 0;
                    c1ReserveGrid.Cols[COL_PAYMENTCLOSEDATE].Width = 100;
                    c1ReserveGrid.Cols[COL_PAYMENTMODE].Width = 0;
                    c1ReserveGrid.Cols[COL_PAYMENTMODENO].Width = 0;
                    c1ReserveGrid.Cols[COL_ASSO_PATIENTID].Width = 0;
                    c1ReserveGrid.Cols[COL_ASSO_PATIENTNAME].Width = 0;
                    c1ReserveGrid.Cols[COL_ASSO_MSTTRANSACTIONID].Width = 0;
                    c1ReserveGrid.Cols[COL_ASSO_TRACKTRANSACTIONID].Width = 0;
                    c1ReserveGrid.Cols[COL_ASSO_CLAIMNO].Width = 100;
                }

                #endregion

                #region " Data Type "

                c1ReserveGrid.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
                c1ReserveGrid.Cols[COL_SOURCE].DataType = typeof(System.String);
                c1ReserveGrid.Cols[COL_COMPANYNAME].DataType = typeof(System.String);
                c1ReserveGrid.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);
                c1ReserveGrid.Cols[COL_USERNAME].DataType = typeof(System.String);
                c1ReserveGrid.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
                c1ReserveGrid.Cols[COL_TYPE].DataType = typeof(System.String);
                c1ReserveGrid.Cols[COL_NOTE].DataType = typeof(System.String);
                c1ReserveGrid.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);
                c1ReserveGrid.Cols[COL_REFUND].DataType = typeof(System.Decimal);
                c1ReserveGrid.Cols[COL_USERESERVE].DataType = typeof(System.Decimal);
                c1ReserveGrid.Cols[COL_PAYMODE].DataType = typeof(System.Int32);
                c1ReserveGrid.Cols[COL_PAYMENTCLOSEDATE].DataType = typeof(System.String);
                c1ReserveGrid.Cols[COL_PAYMENTMODE].DataType = typeof(System.String);
                c1ReserveGrid.Cols[COL_PAYMENTMODENO].DataType = typeof(System.String);
                c1ReserveGrid.Cols[COL_ASSO_PATIENTID].DataType = typeof(System.Int64);
                c1ReserveGrid.Cols[COL_ASSO_PATIENTNAME].DataType = typeof(System.String);
                c1ReserveGrid.Cols[COL_ASSO_MSTTRANSACTIONID].DataType = typeof(System.Int64);
                c1ReserveGrid.Cols[COL_ASSO_TRACKTRANSACTIONID].DataType = typeof(System.Int64);
                c1ReserveGrid.Cols[COL_ASSO_CLAIMNO].DataType = typeof(System.String);

                #endregion

                #region " Alignment "

                c1ReserveGrid.Cols[COL_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_COMPANYNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_ORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_USERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_TORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_NOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_AVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1ReserveGrid.Cols[COL_REFUND].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_USERESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_PAYMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_PAYMENTCLOSEDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_PAYMENTMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1ReserveGrid.Cols[COL_PAYMENTMODENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1ReserveGrid.Cols[COL_ASSO_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_ASSO_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_ASSO_MSTTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_ASSO_TRACKTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ReserveGrid.Cols[COL_ASSO_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                #endregion

                #region " Allow Editing "
                c1ReserveGrid.AllowEditing = false;
                c1ReserveGrid.Cols[COL_EOBPAYMENTID].AllowEditing = false;
                c1ReserveGrid.Cols[COL_SOURCE].AllowEditing = false;//100;
                c1ReserveGrid.Cols[COL_ORIGINALPAYMENT].AllowEditing = false;//100;
                c1ReserveGrid.Cols[COL_USERNAME].AllowEditing = false;//100;
                c1ReserveGrid.Cols[COL_TORESERVES].AllowEditing = false;//100;
                c1ReserveGrid.Cols[COL_TYPE].AllowEditing = false;//100;
                c1ReserveGrid.Cols[COL_NOTE].AllowEditing = false;//100;
                c1ReserveGrid.Cols[COL_AVAILABLE].AllowEditing = false;//100;
                c1ReserveGrid.Cols[COL_REFUND].AllowEditing = false;//100;
                c1ReserveGrid.Cols[COL_USERESERVE].AllowEditing = false;//100;
                c1ReserveGrid.Cols[COL_PAYMODE].AllowEditing = false;//100;
                c1ReserveGrid.Cols[COL_PAYMENTCLOSEDATE].AllowEditing = false;//0;
                c1ReserveGrid.Cols[COL_PAYMENTMODE].AllowEditing = false;//0;
                c1ReserveGrid.Cols[COL_PAYMENTMODENO].AllowEditing = false;//0;
                c1ReserveGrid.Cols[COL_ASSO_PATIENTID].AllowEditing = false;
                c1ReserveGrid.Cols[COL_ASSO_PATIENTNAME].AllowEditing = false;
                c1ReserveGrid.Cols[COL_ASSO_MSTTRANSACTIONID].AllowEditing = false;
                c1ReserveGrid.Cols[COL_ASSO_TRACKTRANSACTIONID].AllowEditing = false;
                c1ReserveGrid.Cols[COL_ASSO_CLAIMNO].AllowEditing = false;

                #endregion

                #region " Set Styles "
                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1ReserveGrid.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1ReserveGrid.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1ReserveGrid.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1ReserveGrid.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                        if (c1ReserveGrid.Name == "c1InsReserveTotal")
                        {
                            csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        }
                    }

                }
                catch
                {
                    csCurrencyStyle = c1ReserveGrid.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    if (c1ReserveGrid.Name == "c1InsReserveTotal")
                    {
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    }

                }
    

                if (c1ReserveGrid.Name == "c1Reserve")
                {
                    C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;// = c1ReserveGrid.Styles.Add("cs_EditableCurrencyStyle");
                    try
                    {
                        if (c1ReserveGrid.Styles.Contains("cs_EditableCurrencyStyle"))
                        {
                            csEditableCurrencyStyle = c1ReserveGrid.Styles["cs_EditableCurrencyStyle"];
                        }
                        else
                        {
                            csEditableCurrencyStyle = c1ReserveGrid.Styles.Add("cs_EditableCurrencyStyle");
                            csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                            csEditableCurrencyStyle.Format = "c";
                        }

                    }
                    catch
                    {
                        csEditableCurrencyStyle = c1ReserveGrid.Styles.Add("cs_EditableCurrencyStyle");
                        csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csEditableCurrencyStyle.Format = "c";


                    }
              
                }
                else if (c1ReserveGrid.Name == "c1InsReserveTotal")
                {
                   // csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                }

                c1ReserveGrid.Cols[COL_TORESERVES].Style = csCurrencyStyle;
                c1ReserveGrid.Cols[COL_AVAILABLE].Style = csCurrencyStyle;
                c1ReserveGrid.Cols[COL_REFUND].Style = csCurrencyStyle;
                c1ReserveGrid.Cols[COL_USERESERVE].Style = csCurrencyStyle;

                #endregion

                c1ReserveGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1ReserveGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                c1ReserveGrid.ShowCellLabels = false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {// _IsFormLoading = false; 
                c1ReserveGrid.Redraw = true; }
        }

        private void DesignPatientRefundgrid(decimal TotalRefund)
        {
            try
            {
                // gloC1FlexStyle.Style(c1PatientRefund, true );
                c1PatientRefund.ShowCellLabels = false;
                #region " Set Header "
                c1PatientRefund.Cols["nRefundID"].Caption = "RefundID";
                c1PatientRefund.Cols["sRefundTo"].Caption = "To";
                c1PatientRefund.Cols["nCloseDate"].Caption = "Close Date";
                c1PatientRefund.Cols["sPaymentTrayDesc"].Caption = "Tray";
                c1PatientRefund.Cols["nRefundDate"].Caption = "Refund Date";
                c1PatientRefund.Cols["nRefundAmount"].Caption = "Amount";
                c1PatientRefund.Cols["sRefundNotes"].Caption = "Note";
                c1PatientRefund.Cols["sUserName"].Caption = "User";
                c1PatientRefund.Cols["dtCreatedDateTime"].Caption = "Date/Time";
                c1PatientRefund.Cols["Status"].Caption = "Status";
                c1PatientRefund.Cols["sPatientName"].Caption = "Patient";
                c1PatientRefund.Cols["nPatientID"].Caption = "nPatientID";
                c1PatientRefund.Cols["Collection Agency"].Caption = "Collection Agency";

                #endregion

                int _nWidth = 0;
                _nWidth = 976;//Convert.ToInt32( c1QueuedClaims.Width);
                c1PatientRefund.Cols["nRefundID"].Width = 0;
                c1PatientRefund.Cols["nRefundID"].Visible = false;

                c1PatientRefund.Cols["nCloseDate"].Width = Convert.ToInt32(_nWidth * 0.08);
                c1PatientRefund.Cols["sPaymentTrayDesc"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1PatientRefund.Cols["sRefundTo"].Width = Convert.ToInt32(_nWidth * 0.14);
                c1PatientRefund.Cols["nRefundDate"].Width = Convert.ToInt32(_nWidth * 0.09);
                c1PatientRefund.Cols["nRefundAmount"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1PatientRefund.Cols["sRefundNotes"].Width = Convert.ToInt32(_nWidth * 0.25);
                c1PatientRefund.Cols["sUserName"].Width = Convert.ToInt32(_nWidth * 0.08);
                c1PatientRefund.Cols["dtCreatedDateTime"].Width = Convert.ToInt32(_nWidth * 0.15);
                c1PatientRefund.Cols["dtCreatedDateTime"].Format = "MM/dd/yyyy hh:mm tt";
                c1PatientRefund.Cols["Status"].Width = Convert.ToInt32(_nWidth * 0.07);
                c1PatientRefund.Cols["nPatientID"].Width = 0;
                
                 if (isBadDebtPatient)
                 {
                     c1PatientRefund.Cols["Collection Agency"].Visible = true;
                     c1PatientRefund.Cols["Collection Agency"].Width = Convert.ToInt32(_nWidth * 0.15);
                 }
                 else
                 {
                     c1PatientRefund.Cols["Collection Agency"].Visible = false;
                     c1PatientRefund.Cols["Collection Agency"].Width =0;
                 }

                c1PatientRefund.Cols["nRefundAmount"].Format = "c";

                c1PatientRefund.Cols["nCloseDate"].DataType = typeof(System.DateTime);
                c1PatientRefund.Cols["nCloseDate"].Format = "MM/dd/yyyy";
                c1PatientRefund.Cols["nRefundDate"].DataType = typeof(System.DateTime);
                c1PatientRefund.Cols["nRefundDate"].Format = "MM/dd/yyyy";


                if (c1PatientRefund.Rows.Count > 1)
                {
                    c1RefundTotal.Cols[0].Width = 0;
                    c1RefundTotal.Cols[1].Width = Convert.ToInt32(_nWidth * 0.10);
                    c1RefundTotal.Cols[2].Width = Convert.ToInt32(_nWidth * 0.08);
                    c1RefundTotal.Cols[3].Width = Convert.ToInt32(_nWidth * 0.10);
                    c1RefundTotal.Cols[4].Width = Convert.ToInt32(_nWidth * 0.14);
                    c1RefundTotal.Cols[5].Width = Convert.ToInt32(_nWidth * 0.08);
                    c1RefundTotal.Cols[6].Width = Convert.ToInt32(_nWidth * 0.10);
                    c1RefundTotal.Cols[7].Width = Convert.ToInt32(_nWidth * 0.25);
                    c1RefundTotal.Cols[8].Width = Convert.ToInt32(_nWidth * 0.09);
                    c1RefundTotal.Cols[9].Width = Convert.ToInt32(_nWidth * 0.15);

                    c1RefundTotal.Cols[1].Caption = "Total :";
                    c1RefundTotal.Cols[6].Caption = "$" + Convert.ToString(TotalRefund);
                    //setGridStyle(c1FlexGridTotalAvailResrv, c1FlexGridTotalAvailResrv.Rows.Count - 1, 11, 25);
                    //setGridStyle(c1RefundTotal, 0, 0, 6);
                }
                else
                {
                    c1RefundTotal.Cols[0].Width = 1300;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }

        }

        private void DesignRefundGrid(C1FlexGrid c1RefundsGrid)
        {

            try
            {
                #region " Grid Settings "

                c1RefundsGrid.Redraw = false;
                c1RefundsGrid.Clear();

                c1RefundsGrid.Cols.Count = COL_COUNT_REF;
                c1RefundsGrid.Rows.Count = 1;
                c1RefundsGrid.Rows.Fixed = 1;
                c1RefundsGrid.Cols.Fixed = 0;

                c1RefundsGrid.AllowEditing = false;
                c1RefundsGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                if (c1RefundsGrid.Name == "c1InsuranceRefundLog")
                {
                    c1RefundsGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;
                }
                else if (c1RefundsGrid.Name == "c1InsRefundTotal")
                {
                    c1RefundsGrid.AllowSorting = AllowSortingEnum.None;
                }


                #endregion

                #region " Set Headers "
                if (c1RefundsGrid.Name == "c1InsuranceRefundLog")
                {

                    c1RefundsGrid.SetData(0, COL_CLOSEDATE_REF, "Close Date");
                    c1RefundsGrid.SetData(0, COL_TRAY_REF, "Tray");
                    c1RefundsGrid.SetData(0, COL_COMPANY_REF, "Refund To");
                    c1RefundsGrid.SetData(0, COL_CHECK_NO_REF, "Refund Check#");
                    c1RefundsGrid.SetData(0, COL_REFUND_DATE_REF, "Refund Date");
                    c1InsuranceRefundLog.SetData(0, COL_REFUND_AMOUNT_REF, "Amount");
                    c1RefundsGrid.SetData(0, COL_USER_REF, "User");
                    c1RefundsGrid.SetData(0, COL_NOTE_REF, "Note");
                    c1RefundsGrid.SetData(0, COL_DATETIME_REF, "Date / Time");
                    c1RefundsGrid.SetData(0, COL_STATUS_REF, "Status");
                    c1RefundsGrid.SetData(0, COL_REFUNDID_REF, "nRefundID");
                    c1RefundsGrid.SetData(0, COL_CLAIMNO_REF, "Claim #");
                    c1RefundsGrid.SetData(0, COL_PATIENTNAME_REF, "nRefundID");
                }
                #endregion

                #region " Show/Hide "

                c1RefundsGrid.Cols[COL_EOBPAYMENT_ID_REF].Visible = false;
                c1RefundsGrid.Cols[COL_COMPANY_ID_REF].Visible = false;
                c1RefundsGrid.Cols[COL_PAYMENT_TRAY_ID_REF].Visible = false;
                c1RefundsGrid.Cols[COL_USER_ID_REF].Visible = false;
                c1RefundsGrid.Cols[COL_DEBIT_AMOUNT_REF].Visible = false;
                c1RefundsGrid.Cols[COL_REFUNDID_REF].Visible = false;
                c1RefundsGrid.Cols[COL_DATETIME_REF].Visible = false;
                c1RefundsGrid.Cols[COL_PATIENTNAME_REF].Visible = false;

                #endregion

                #region " Width "

                c1RefundsGrid.Cols[COL_CLOSEDATE_REF].Width = 100;
                c1RefundsGrid.Cols[COL_TRAY_REF].Width = 90;
                c1RefundsGrid.Cols[COL_COMPANY_REF].Width = 150;
                c1RefundsGrid.Cols[COL_COMPANY_ID_REF].Width = 0;
                c1RefundsGrid.Cols[COL_CHECK_NO_REF].Width = 100;
                c1RefundsGrid.Cols[COL_REFUND_DATE_REF].Width = 100;
                c1RefundsGrid.Cols[COL_REFUND_AMOUNT_REF].Width = 90;
                c1RefundsGrid.Cols[COL_USER_REF].Width = 150;
                c1RefundsGrid.Cols[COL_NOTE_REF].Width = 280;
                c1RefundsGrid.Cols[COL_STATUS_REF].Width = 100;
                c1RefundsGrid.Cols[COL_DATETIME_REF].Width = 0;
                c1RefundsGrid.Cols[COL_CLAIMNO_REF].Width = 100;
                c1RefundsGrid.Cols[COL_PATIENTNAME_REF].Width = 0;

                #endregion

                #region " Data Type "

                c1RefundsGrid.Cols[COL_CLOSEDATE_REF].DataType = typeof(System.String);
                c1RefundsGrid.Cols[COL_TRAY_REF].DataType = typeof(System.String);
                c1RefundsGrid.Cols[COL_COMPANY_REF].DataType = typeof(System.String);
                c1RefundsGrid.Cols[COL_CHECK_NO_REF].DataType = typeof(System.String);

                c1RefundsGrid.Cols[COL_REFUND_DATE_REF].DataType = typeof(System.String);
                c1RefundsGrid.Cols[COL_NOTE_REF].DataType = typeof(System.String);
                c1RefundsGrid.Cols[COL_STATUS_REF].DataType = typeof(System.String);
                c1RefundsGrid.Cols[COL_DATETIME_REF].DataType = typeof(System.String);
                c1RefundsGrid.Cols[COL_REFUNDID_REF].DataType = typeof(System.Object);
                c1RefundsGrid.Cols[COL_CLAIMNO_REF].DataType = typeof(System.String);
                c1RefundsGrid.Cols[COL_PATIENTNAME_REF].DataType = typeof(System.String);

                #endregion

                #region " Alignment "

                c1RefundsGrid.Cols[COL_COMPANY_REF].TextAlign = TextAlignEnum.LeftCenter;
                c1RefundsGrid.Cols[COL_CHECK_NO_REF].TextAlign = TextAlignEnum.LeftCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1RefundsGrid.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1RefundsGrid.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1RefundsGrid.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1RefundsGrid.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont ;//new System.Drawing.Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                        //csCurrencyStyle.ForeColor = Color.Blue;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1RefundsGrid.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont ;//new System.Drawing.Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    //csCurrencyStyle.ForeColor = Color.Blue;

                }
    
      

                c1RefundsGrid.Cols[COL_REFUND_AMOUNT_REF].Style = csCurrencyStyle;

                if (c1RefundsGrid.Name == "c1InsuranceRefundLog")
                {
                    csCurrencyStyle.BackColor = Color.White;
                }
                else if (c1RefundsGrid.Name == "c1InsRefundTotal")
                {
                    csCurrencyStyle.ForeColor = Color.Blue;
                }
                #endregion
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally
            { c1RefundsGrid.Redraw = true; }
        }
        #endregion


        private void SetNoteFlag()
        {
          //  bool isNoteFlag = false;
            try
            {

                //c1FlexGridChargesClaims.FinishEditing();
                //c1FlexGridChargesClaims.Redraw = false;
               
                // c1FlexGridChargesClaims.SetCellImage(0, COL_NotesCount, global::gloBilling.Properties.Resources.Notes);
                c1FlexGridChargesClaims.Cols["blnNoteFlag"].DataType = typeof(Image);
                c1FlexGridChargesClaims.Cols["blnNoteFlag"].ImageMap = new System.Collections.Hashtable();
                c1FlexGridChargesClaims.Cols["blnNoteFlag"].ImageMap.Add(0, null);
                c1FlexGridChargesClaims.Cols["blnNoteFlag"].ImageMap.Add(1, global::gloBilling.Properties.Resources.Notes);
                c1FlexGridChargesClaims.Cols["blnNoteFlag"].ImageAndText = false;
                c1FlexGridChargesClaims.Cols["blnNoteFlag"].AllowResizing = false;
                //foreach (Row myRow in c1FlexGridChc1FlexGridChargesClaims.Cols["Note"].DataTypeargesClaims.Rows)
                //{
                //    isNoteFlag = false;

                //    if (myRow.Index == 0)
                //    { continue; }

                //    object noteFlag = c1FlexGridChargesClaims.GetData(myRow.Index, c1FlexGridChargesClaims.Cols.IndexOf("blnNoteFlag"));

                //    if (!noteFlag.Equals(System.DBNull.Value))
                //    {
                //        if (Convert.ToBoolean(c1FlexGridChargesClaims.GetData(myRow.Index, c1FlexGridChargesClaims.Cols.IndexOf("blnNoteFlag"))))
                //        { isNoteFlag = true; }
                //    }

                //    if (isNoteFlag)
                //    {
                //        // Set the Note Image
                //        System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Notes;
                //        this.c1FlexGridChargesClaims.SetCellImage(myRow.Index, COL_NOTE_IMAGE, imgFlag);
                //    }
                //    else
                //    {
                //        // Clear the Note Image
                //        System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.None;
                //        this.c1FlexGridChargesClaims.SetCellImage(myRow.Index, COL_NOTE_IMAGE, null);
                //    }

                //    noteFlag = null;
                //}

                //if (c1FlexGridChargesClaims.Rows.Count > 1)
                //{
                //    if (c1FlexGridChargesClaims.Rows.Count >= iChargesSelRow)
                //    {
                //        c1FlexGridChargesClaims.Row = iChargesSelRow;
                //    }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);               
            }
            //c1FlexGridChargesClaims.Redraw = true;
            //c1FlexGridChargesClaims.StartEditing();
        }

        private void c1FlexGridChargesClaims_AfterSort(object sender, SortColEventArgs e)
        {
            if (c1FlexGridChargesClaims.Rows.Count > 1)
            {
                //for (int i = 1; i <= c1FlexGridChargesClaims.Rows.Count - 1; i++)
                //{
                //    setNormalGridStyle(i);
                //}
                if (e.Col == c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)
                {
                    c1FlexGridChargesClaims.Cols["SortClaim"].Sort = e.Order;
                    c1FlexGridChargesClaims.Cols["SortSubClaim"].Sort = SortFlags.Ascending;
                    c1FlexGridChargesClaims.Sort(SortFlags.UseColSort, c1FlexGridChargesClaims.Cols["SortClaim"].Index, c1FlexGridChargesClaims.Cols["SortSubClaim"].Index);
                }

                SetNoteFlag();
                FillClaimOnHold();
                int _index;
                try
                {
                    _index = c1FlexGridChargesClaims.FindRow(_PaChargesID.ToString(), 0, c1FlexGridChargesClaims.Cols["nTransactionDetailMSTID"].Index, false, false, false);
                 //   _index = c1FlexGridChargesClaims.FindRow(_PaChargesID.ToString(), 0, 0, false);
                }
                catch (Exception)
                {
                    _index = 0;
                }

                c1FlexGridChargesClaims.ShowCell(_index, 0);
                c1FlexGridChargesClaims.Row = _index;
                c1FlexGridChargesClaims.Select();
            }
        }              

        #region "c1PALogView Events"

        private void c1PALogView_DoubleClick(object sender, EventArgs e)
        {
            if (c1PALogView.MouseRow > 0)
            {
                ApplyDrillDown();
            }
        }

        private void c1PALogView_MouseLeave(object sender, EventArgs e)
        {
            if (filter.Filters.Count == 0)
            {
                c1PALogView.SetCellStyle(0, (int)GridColumnAccountLog.Type, "HeaderFilterIcon");
            }

            C1SuperTooltip1.Hide(); 
        }

        private void c1PALogView_MouseMove(object sender, MouseEventArgs e)
        {
            if (c1PALogView.MouseCol == (int)GridColumnAccountLog.Type && c1PALogView.MouseRow == 0)
            {
                c1PALogView.SetCellStyle(0, (int)GridColumnAccountLog.Type, "RemoveHeaderFilterIcon");
            }

            if (c1PALogView.MouseCol == (int)GridColumnAccountLog.Patient)
            {
                gloBilling.gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
            }
        }

        private void c1PALogView_MouseLeaveCell(object sender, RowColEventArgs e)
        {
            if (filter.Filters.Count == 0)
            {
                c1PALogView.SetCellStyle(0, (int)GridColumnAccountLog.Type, "HeaderFilterIcon");
            }
        }

        private void c1PALogView_AfterFilter(object sender, EventArgs e)
        {
            if (filter != null)
            {
                if (filter.Filters.Count > 0)
                {
                    c1PALogView.SetCellStyle(0, (int)GridColumnAccountLog.Type, "RemoveHeaderFilterIcon");
                }
                else
                {
                    c1PALogView.SetCellStyle(0, (int)GridColumnAccountLog.Type, "HeaderFilterIcon");
                }
            }
        } 
        #endregion

        private void tsp_AccFollowUp_Click(object sender, EventArgs e)
        {
            frmSetupFollowupDateAction objFollowup = new frmSetupFollowupDateAction(CollectionEnums.FollowUpType.PatientAccount, oPatientControl.PatientID, oPatientControl.PAccountID,oPatientControl.AccountPatientID);
            objFollowup.CallingContainer = this;
            objFollowup.ShowDialog(this);
            objFollowup.Dispose();
            objFollowup = null;
            tsb_Refresh_Click(null, null);
        }

        private void tsb_StatementCount_Click(object sender, EventArgs e)
        {
            frmStatementCount objStatementCount = new frmStatementCount();
            objStatementCount.PAccountID = _nPAccountId;
            objStatementCount.PatientID = _nPatientID;
            objStatementCount.AccountPatientID = _nAccountPatientId;
            objStatementCount.ShowDialog(this);
            objStatementCount.Dispose();
            objStatementCount = null;
            oPatientControl.RefreshData();
        }

        private void chkIncludeVoids_CheckedChanged(object sender, EventArgs e)
        {
            DataView _dv = null;
            try
            {
                _dv = (DataView)c1FlexGridChargesClaims.DataSource;
                if (_dv != null)
                {
                    if (chkIncludeVoids.Checked == true)
                    {
                        _dv.RowFilter = "";
                        c1FlexGridChargesClaims.DataSource = _dv;
                        c1FlexGridChargesClaims.Refresh();
                        if (_dv.Count > 0)
                        {
                            tsbViewHistory.Visible = true;
                        }
                        else
                        {
                            tsbViewHistory.Visible = false;
                        }

                        CalculateTotalBalances(_dv);
                        SetNoteFlag();
                        FillClaimOnHold();
                    }
                    else
                    {
                        _dv.RowFilter = "";
                        _dv.RowFilter = "isnull(Party,'0') <> 'V'";
                        c1FlexGridChargesClaims.DataSource = _dv;
                        c1FlexGridChargesClaims.Refresh();
                        if (_dv.Count > 0)
                        {
                            tsbViewHistory.Visible = true;
                        }
                        else
                        {
                            tsbViewHistory.Visible = false;
                        }

                        CalculateTotalBalances(_dv);
                        SetNoteFlag();
                        FillClaimOnHold();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        
       private void c1FlexGrid_Statements_AfterSort(object sender, SortColEventArgs e)
        { // Code added for sorting on statement date  
           if (e.Col == 0)
            {
                if ((e.Order & C1.Win.C1FlexGrid.SortFlags.Descending) != 0)
                {
                    c1FlexGrid_Statements.Sort(C1.Win.C1FlexGrid.SortFlags.Descending, 14);
                }
                else
                {
                    c1FlexGrid_Statements.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, 14);
                }
            }

            if (c1FlexGrid_Statements != null && c1FlexGrid_Statements.Cols.Count > 0)
            {
                for (int colInd = 0; colInd < c1FlexGrid_Statements.Cols.Count; colInd++)
                {
                    if (c1FlexGrid_Statements.Cols[colInd].Name != "Note")
                    {
                        c1FlexGrid_Statements.Cols[colInd].AllowEditing = false;
                    }
                    else
                    {
                        c1FlexGrid_Statements.Cols[colInd].AllowEditing = true;
                    }
                }
            }


            if (c1FlexGrid_Statements != null && c1FlexGrid_Statements.Rows.Count > 0)
            {
                for (int RowInd = 1; RowInd < c1FlexGrid_Statements.Rows.Count; RowInd++)
                {
                    if (Convert.ToString(c1FlexGrid_Statements.Rows[RowInd]["Status"].ToString()) != "Voided")
                    {
                        c1FlexGrid_Statements.Rows[RowInd].AllowEditing = false;
                    }
                    else
                    {
                        c1FlexGrid_Statements.Rows[RowInd].AllowEditing = true;
                    }
                }
            }

            int _index;
            try
            {
                _index = c1FlexGrid_Statements.FindRow(_StatementID.ToString(), 0, c1FlexGrid_Statements.Cols["nBatchPateintStatMstID"].Index, false, false, false);
            }
            catch (Exception)
            {
                _index = 0;
            }

            c1FlexGrid_Statements.ShowCell(_index, 0);
            c1FlexGrid_Statements.Row = _index;
            c1FlexGrid_Statements.Select();
        }
       Int64 _PaChargesID;
       private void c1FlexGridChargesClaims_BeforeSort(object sender, SortColEventArgs e)
       {
           if (c1FlexGridChargesClaims.Rows.Count > 1)
           {
               try
               {
                   _PaChargesID = Convert.ToInt64(c1FlexGridChargesClaims.Rows[c1FlexGridChargesClaims.RowSel]["nTransactionDetailMSTID"]);
                           }
               catch (Exception)
               {
                   _PaChargesID = 0;
               }
           
           }
       }
       Int64 _PaymetID;
       private void c1FlexGridPmnt_BeforeSort(object sender, SortColEventArgs e)
       {
           if (c1FlexGridPmnt.Rows.Count > 1)
           {
               try
               {
                   _PaymetID = Convert.ToInt64(c1FlexGridPmnt.Rows[c1FlexGridPmnt.RowSel]["nEOBPaymentID"]);
               }
               catch (Exception)
               {
                   _PaymetID = 0;
               }
           }
       }

       private void c1FlexGridPmnt_AfterSort(object sender, SortColEventArgs e)
       {
           int _index;
           try
           {
               _index = c1FlexGridPmnt.FindRow(_PaymetID.ToString(), 0, c1FlexGridPmnt.Cols["nEOBPaymentID"].Index, false, false, false);
           }
           catch (Exception)
           {
               _index = 0;
           }

           c1FlexGridPmnt.ShowCell(_index, 0);
           c1FlexGridPmnt.Row = _index;
           c1FlexGridPmnt.Select();
       }
      
       Int64 _ReserveID; 
       private void c1FlexGridAvailResrv_BeforeSort(object sender, SortColEventArgs e)
       {

           if (c1FlexGridAvailResrv.Rows.Count > 1)
           {
               try
               {
                   _ReserveID = Convert.ToInt64(c1FlexGridAvailResrv.Rows[c1FlexGridAvailResrv.RowSel]["nEOBPaymentID"]);
               }
               catch (Exception)
               {
                   _ReserveID = 0;
               }
           }
       }

       private void c1FlexGridAvailResrv_AfterSort(object sender, SortColEventArgs e)
       {
           int _index;
           try
           {
               _index = c1FlexGridAvailResrv.FindRow(_ReserveID.ToString(), 0, c1FlexGridAvailResrv.Cols["nEOBPaymentID"].Index, false, false, false);
           }
           catch (Exception)
           {
               _index = 0;
           }

           c1FlexGridAvailResrv.ShowCell(_index, 0);
           c1FlexGridAvailResrv.Row = _index;
           c1FlexGridAvailResrv.Select();
       }
      
       Int64 _RefundID;
       private void c1PatientRefund_BeforeSort(object sender, SortColEventArgs e)
       {
           if (c1PatientRefund.Rows.Count > 1)
           {
               try
               {
                   _RefundID = Convert.ToInt64(c1PatientRefund.Rows[c1PatientRefund.RowSel]["nRefundID"]);
               }
               catch (Exception)
               {
                   _RefundID = 0;
               }
           }
       }

       private void c1PatientRefund_AfterSort(object sender, SortColEventArgs e)
       {
           int _index;
           try
           {
               _index = c1PatientRefund.FindRow(_RefundID.ToString(), 0, c1PatientRefund.Cols["nRefundID"].Index, false, false, false);
           }
           catch (Exception)
           {
               _index = 0;
           }

           c1PatientRefund.ShowCell(_index, 0);
           c1PatientRefund.Row = _index;
           c1PatientRefund.Select();
       }
       Int64 _StatementID;
       private void c1FlexGrid_Statements_BeforeSort(object sender, SortColEventArgs e)
       {
           if (c1FlexGrid_Statements.Rows.Count > 1)
           {
               try
               {
                   _StatementID = Convert.ToInt64(c1FlexGrid_Statements.Rows[c1FlexGrid_Statements.RowSel]["nBatchPateintStatMstID"]);
               }
               catch (Exception)
               {
                   _StatementID = 0;
               }
           }
       }
       Int64 _GlobalPeriodID; 
       private void c1GlobalPeriods_BeforeSort(object sender, SortColEventArgs e)
       {
           if (c1GlobalPeriods.Rows.Count > 1)
           {
               try
               {
                   _GlobalPeriodID = Convert.ToInt64(c1GlobalPeriods.Rows[c1GlobalPeriods.RowSel]["ID"]);
               }
               catch (Exception)
               {
                   _GlobalPeriodID = 0;
               }
           }
       }

       private void c1GlobalPeriods_AfterSort(object sender, SortColEventArgs e)
       {
           if (e.Col == c1GlobalPeriods.Cols["Dates"].Index)
           {
               if ((e.Order & C1.Win.C1FlexGrid.SortFlags.Descending) != 0)
               {
                   c1GlobalPeriods.Sort(C1.Win.C1FlexGrid.SortFlags.Descending, c1GlobalPeriods.Cols["StartDate"].Index);
               }
               else
               {
                   c1GlobalPeriods.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, c1GlobalPeriods.Cols["StartDate"].Index);

               }
           }
           int _index;
           try
           {
               _index = c1GlobalPeriods.FindRow(_GlobalPeriodID.ToString(), 0, c1GlobalPeriods.Cols["ID"].Index, false, false, false);
           }
           catch (Exception)
           {
               _index = 0;
           }

           c1GlobalPeriods.ShowCell(_index, 0);
           c1GlobalPeriods.Row = _index;
           c1GlobalPeriods.Select();
       }

       private void c1FlexGridChargesClaims_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
       {
           if (e.Row > 0 && e.Col == c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)
           {
               if ((strArrClaims != null) && (strArrClaims.Contains(c1FlexGridChargesClaims.GetData(e.Row, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString())))
               {
                   c1FlexGridChargesClaims.Rows[e.Row].Style = c1FlexGridChargesClaims.Styles["SubCol"]; ;
               }
               else
               {

                   c1FlexGridChargesClaims.Rows[e.Row].Style = c1FlexGridChargesClaims.Styles["csSubColTest"]; ;
               }
           }
       }

       private void rbClaim_CheckedChanged(object sender, EventArgs e)
       {
           if (rbClaim.Checked == true)
           {

               if (tbpgReport.Tag.ToString() != "isReportLoaded")
               {
                   GenerateReport();
               }

               rbClaim.Font = _fBold; // new Font("Tahoma", 9, FontStyle.Bold);
           }
           else
           {
               rbClaim.Font = _fRegular; // new Font("Tahoma", 9, FontStyle.Regular);
           }
       }

       private void rbPayment_CheckedChanged(object sender, EventArgs e)
       {
           if (rbPayment.Checked == true)
           {
               if (tbpgReport.Tag.ToString() != "isPaymentReportLoaded")
               {
                   GenerateReport();
               }

               rbPayment.Font = _fBold; // new Font("Tahoma", 9, FontStyle.Bold);
           }
           else
           {
               rbPayment.Font = _fRegular; //new Font("Tahoma", 9, FontStyle.Regular);
           }
       }

       private void tsb_btnGWStatement_Click(object sender, EventArgs e)
       {
           string sSelectedStatementDate = null;
           DateTime dtStmCreateDate;

           if (c1FlexGrid_Statements.Rows.Count > 1)
           {
               if (c1FlexGrid_Statements.RowSel < c1FlexGrid_Statements.Rows.Count)
               {
                   if (c1FlexGrid_Statements.Rows.Selected != null)
                   {
                       if (c1FlexGrid_Statements.RowSel > 0)
                       {
                           sSelectedStatementDate = Convert.ToString(c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, 14));
                           dtStmCreateDate = Convert.ToDateTime(c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, 1));

                           gloPatient.gloAccount objgloAccount = null;

                           if (_nPatientID != 0 && _nPAccountId != 0)
                           {
                               gloBilling.Statement.frmRpt_Revised_PatientStatement frmPatStmt = new global::gloBilling.Statement.frmRpt_Revised_PatientStatement(gloPMGlobal.DatabaseConnectionString, Convert.ToInt64(c1FlexGrid_Statements.GetData(c1FlexGrid_Statements.RowSel, 9)), _nPAccountId, true, true, sSelectedStatementDate, dtStmCreateDate);//
                               frmPatStmt.on_FromClose += new global::gloBilling.Statement.frmRpt_Revised_PatientStatement.onFromClose(frmPatStmt_on_FromClose);
                               frmPatStmt.WindowState = FormWindowState.Maximized;
                               objgloAccount = new gloPatient.gloAccount(gloPMGlobal.DatabaseConnectionString);
                               frmPatStmt.IsPatientAccountEnable = objgloAccount.GetPatientAccountFeatureSetting();
                               if (oPatientControl.IsAllAccPatSelected)
                               {
                                   frmPatStmt.IsAllAccPatSelected = true;
                               }

                               if (!IsCalledFromInsPmt)
                               { frmPatStmt.IsCalledFromPatAcct = true; frmPatStmt.MdiParent = this.ParentForm; frmPatStmt.Show(); frmPatStmt.generateIndividualStmt(); }
                               else
                               { frmPatStmt.IsCalledFromPatAcct = true; frmPatStmt.IsCalledFromInsPmt = IsCalledFromInsPmt; frmPatStmt.ShowDialog(this); frmPatStmt.Dispose(); frmPatStmt = null; }
                               if (frmPatStmt != null)
                               {
                                   foreach (Form myForm in Application.OpenForms)
                                   {

                                       if (myForm.TopMost)
                                       {
                                           myForm.TopMost = false;
                                       }

                                   }
                                   frmPatStmt.TopMost = true;
                               }
                           }
                       }
                   }
               }

           }
           else
           {
               MessageBox.Show("Statement details are not available.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
           }

       }

       private void tsb_ViewExamNote_Click(object sender, EventArgs e)
       {
           if (c1FlexGridChargesClaims.Rows.Count > 1)
           {
               string sDos = Convert.ToString(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["DOS"].Index));//Convert.ToString(gloDateMaster.gloDate.DateAsDate((UC_gloBillingTransactionLines.GetMinDOS())));
               string sProvider = Convert.ToString(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nProviderName"].Index));//Convert.ToString(gloDateMaster.gloDate.DateAsDate((UC_gloBillingTransactionLines.GetMinDOS())));
               Int64 nPatientID = Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["PatientID"].Index));//Convert.ToString(gloDateMaster.gloDate.DateAsDate((UC_gloBillingTransactionLines.GetMinDOS())));
               frmBillingPatientviewExam oPatientExam = new frmBillingPatientviewExam(gloPMGlobal.DatabaseConnectionString, nPatientID, sDos, sProvider);
               oPatientExam.ShowDialog(this);
               oPatientExam.Dispose();
           }
       }

       private void tsb_btnTransferResponsibility_Click(object sender, EventArgs e)
       {
           Int64 nPatientID = _nSelectedPatientId;
           Int64 nAccountID = _nPAccountId;

           frmPatientClaimsList ofrmPatientClaimsList = new frmPatientClaimsList();
           ofrmPatientClaimsList.PatientID = nPatientID;
           ofrmPatientClaimsList.PatientAccountID = nAccountID;
           ofrmPatientClaimsList.ShowDialog(this);
           tsb_Refresh_Click(null, null);
       }

       private void tsb_btnCollAgencyRefund_Click(object sender, EventArgs e)
       {
           try
           {
             
               gloAccountsV2.frmCollectionAgencyPaymentRefund ofrmPatientRefundvoid = new gloAccountsV2.frmCollectionAgencyPaymentRefund(gloPMGlobal.DatabaseConnectionString);
               try
               {
                   ofrmPatientRefundvoid.SelectedPatientId = _nPatientID;
                   ofrmPatientRefundvoid.ShowDialog(this);
               }
               catch (Exception ex)
               {
                   gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
               }
               finally
               {
                   if (ofrmPatientRefundvoid != null) { ofrmPatientRefundvoid.Dispose(); ofrmPatientRefundvoid = null; }                 
               }
           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
           }
          
       }

       private void ts_btnPrint_Click(object sender, EventArgs e)
       {

           if (PAHreportViewer.CurrentStatus.CanRefreshData == false)
           {
               MessageBox.Show("Report is not generated. Generate report before print.", "gloPM",MessageBoxButtons.OK, MessageBoxIcon.Information);
               return;
           }
           else
           {
               printReport();
           }
       }      
        #region "Commented Code "

       private void printReport()
       {
           string sqlServerName = string.Empty;
           string sqlDatabaseName = string.Empty;
           string sqlUser = string.Empty;
           string sqlPwd = string.Empty;
           gloSSRSApplication.clsPrintReport clsPrntRpt = null;
           string PDFFileName = "";
           try
           {
               sqlServerName = Convert.ToString(appSettings["SQLServerName"]);
               sqlDatabaseName = Convert.ToString(appSettings["DataBaseName"]);
               sqlUser = Convert.ToString(appSettings["SQLLoginName"]);
               sqlPwd = Convert.ToString(appSettings["SQLPassword"]);

               bool blSQLAuth = !(Convert.ToBoolean(appSettings["WindowAuthentication"]));
               bool gblnIsDefaultPrinter = !(Convert.ToBoolean(appSettings["DefaultPrinter"]));

               clsPrntRpt = new gloSSRSApplication.clsPrintReport(sqlServerName, sqlDatabaseName, blSQLAuth,sqlUser, sqlPwd);
               if (!(gloGlobal.gloTSPrint.isCopyPrint && gloGlobal.gloTSPrint.UseEMFForSSRS))
               {
                   PDFFileName = ConvertSSRStoPDF(_reportName);
               }
               clsPrntRpt.PrintReport(_reportName, _parameterName, _ParameterValue, gblnIsDefaultPrinter, "",PDFFileName, PAHreportViewer.ServerReport);

           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
               ex = null;

           }
           finally
           {
               //  Cleanup for used variables under this method.
               sqlServerName = string.Empty;
               sqlDatabaseName = string.Empty;
               sqlUser = string.Empty;
               sqlPwd = string.Empty;

               if (clsPrntRpt != null)
               {
                   clsPrntRpt.Dispose();
                   clsPrntRpt = null;
               }
           }
       }
       private string ConvertSSRStoPDF(string RptName)
       {
           try
           {
               Warning[] warnings = null;
               string[] streamids = null;
               string mimeType = null;
               string encoding = null;
               string extension = null;
               byte[] bytes = null;
               string Format = null;


               Format = "PDF";
               bytes = this.PAHreportViewer.ServerReport.Render(Format, null, out mimeType, out encoding, out extension, out streamids, out warnings);
               gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
               string _FileName = "";
               _FileName = gloSettings.FolderSettings.AppTempFolderPath + Guid.NewGuid().ToString() + ".PDF";
               FileStream fs = new FileStream(_FileName, FileMode.Create);
               fs.Write(bytes, 0, bytes.Length);
               fs.Close();
               fs.Dispose();
               fs = null;

               gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports,gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, RptName + " Usage Report Printed..", 0,0, 0, gloAuditTrail.ActivityOutCome.Success);
               return _FileName;
               // Print(_FileName);
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
               return "";
           }
       }

       #region Cleargage
       private void tsb_Pat_OA_AddPaymentPlan_Click(object sender, EventArgs e)
       {
           //try
           //{
           //    //GetAccountDue();
           //    DataTable dtPatientEncounters = GetPatientEncouter(_nPatientID,_nPAccountId);
           //    if (dtPatientEncounters != null && dtPatientEncounters.Rows.Count > 0)
           //    {
           //        ClearGage.SSO.Patient oPatient = GetPatientInfo(_nPatientID);
           //        Double dPatientDue = Convert.ToDouble(GetAccountDue());
           //        if (dPatientDue == 0)
           //        {
           //            MessageBox.Show("Add payment plan is available for patient having patient due.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
           //        }
           //        else
           //        {
           //            string content = ssoHelper.GetAddPaymentPlanDialogHtml(oPatient, dPatientDue);
           //            DisplayWebBrowser(content, "Add Payment Plan");
           //        }
           //    }
           //    else
           //    {
           //        MessageBox.Show("Encounter information already send to cleargage.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
           //    }
           //}
           //catch (Exception ex)
           //{
           //    MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
           //}
       }

       private DataTable GetPatientEncouter(long _nPatientID, long _nPAccountID)
       {
           DataTable dt = null;
           //ClsCleargagePaymentPosting clsCleargagePosting = null;
           //try
           //{
           //    //ClearGage.SSO.Response.Transaction[] oOTPTransactions = (ClearGage.SSO.Response.Transaction[])obj;
           //    //Int64 nOTPTransactionID = 0;
           //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.Generate, "Recent Transaction callback start", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
           //    clsCleargagePosting = new ClsCleargagePaymentPosting();

           //    dt=clsCleargagePosting.GetPatientEncounterDetails(_nPatientID, _nPAccountID);
           //    if (dt!=null&&dt.Rows.Count>0)
           //    {
           //        frmCGPaymentPlanList oFrmCGPaymentPlanList = new frmCGPaymentPlanList();
           //        oFrmCGPaymentPlanList.dtEncounterInfo = dt;
           //        oFrmCGPaymentPlanList.ShowDialog();
           //    }
           //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.Generate, "Recent Transaction callback end", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

           //}
           //catch (Exception ex)
           //{
           //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.Generate, "Exception in Recent Transaction callback: " + ex.ToString(), _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
           //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
           //}
           //finally
           //{
           //    if (clsCleargagePosting != null)
           //    {
           //        clsCleargagePosting.Dispose();
           //        clsCleargagePosting = null;
           //    }
           //}
           return dt;
       }
       private decimal GetAccountDue()
       {

           decimal dLastPatPayment = 0;
           decimal dTotalBalAmt = 0;
           decimal dTotalInsPending = 0;
           decimal dTotalPatientDue = 0;
           decimal dTotalcopayReserve = 0;
           decimal dTotalAdvancedReserve = 0;
           decimal dTotalOtherReserve = 0;
           decimal dTotalBadDebtDue = 0;
           string sLastPatPaymentDate = "";

           DataSet dtSet = null;
           DataTable dtInsuranceDetails = null;
           DataTable dtReserveDetails = null;
           try
           {
               
               dtSet= gloStripControl.PatientStripControl.GetPatientBalances(_nPatientID, _nPAccountId);

               dtInsuranceDetails = dtSet.Tables[0];
               dtReserveDetails = dtSet.Tables[1];

               if (dtInsuranceDetails != null && dtInsuranceDetails.Rows.Count > 0)
               {
                   //dTotalBalAmt = dtInsuranceDetails.Rows[0]["TotalBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["TotalBalance"]);
                   dTotalInsPending = dtInsuranceDetails.Rows[0]["InsuranceDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["InsuranceDue"]);
                   dTotalPatientDue = dtInsuranceDetails.Rows[0]["PatientDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["PatientDue"]);
                   dTotalBadDebtDue = dtInsuranceDetails.Rows[0]["BadDebtDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["BadDebtDue"]);
                   dLastPatPayment = dtInsuranceDetails.Rows[0]["PatientLastPay"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["PatientLastPay"]);
                   sLastPatPaymentDate = dtInsuranceDetails.Rows[0]["LastPayDate"] == DBNull.Value ? "" : Convert.ToDateTime(dtInsuranceDetails.Rows[0]["LastPayDate"]).ToString("MM/dd/yyyy");
                   if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                   {
                       dTotalBalAmt = dTotalInsPending + dTotalPatientDue + dTotalBadDebtDue;
                   }
                   else
                   {
                       dTotalBalAmt = dTotalInsPending + dTotalPatientDue;
                   }
               }
               //Assign Copay Reserve,AdvancedResere,OtherReserve to Varialbles
               if (dtReserveDetails != null && dtReserveDetails.Rows.Count > 0)
               {
                   foreach (DataRow drReserveDetails in dtReserveDetails.Rows)
                   {
                       if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 2)   //For Copay Reserve
                       {
                           dTotalcopayReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                       }

                       if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 3)  //ForAdvanced Reserve
                       {
                           dTotalAdvancedReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                       }
                       if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 4) //For OtherReserve
                       {
                           dTotalOtherReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                       }
                   }
               }
               dTotalBalAmt = dTotalBalAmt - (dTotalcopayReserve + dTotalAdvancedReserve + dTotalOtherReserve);
               dTotalPatientDue = dTotalPatientDue - (dTotalcopayReserve + dTotalAdvancedReserve + dTotalOtherReserve);

           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
           }
           finally
           {
               if (dtInsuranceDetails != null)
                   dtInsuranceDetails.Dispose();
               if (dtReserveDetails != null)
                   dtReserveDetails.Dispose();
               if (dtSet != null)
               {
                   dtSet.Dispose();
                   dtSet = null;
               }
           }
           return dTotalPatientDue;
       }
       private void tsb_Pat_OA_EditPaymentPlan_Click(object sender, EventArgs e)
       {
           gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Edit payment plan start", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

           try
           {
               if (c1PaymentPlan.DataSource != null && c1PaymentPlan.Rows.Count > 1)
               {
                   string sPlanID = Convert.ToString(c1PaymentPlan.GetData(c1PaymentPlan.RowSel, 1));
                   string content = ssoHelper.GetEditPaymentPlanDialogHtml(sPlanID);
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Edit payment plan PlanID: " + sPlanID, _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                   DisplayWebBrowser(content, "Edit Payment Plan");
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanEnd, "Edit payment plan PlanID: " + sPlanID, _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
               }
               else
               {
                   MessageBox.Show("No payment plan is available for edit.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanEnd, "No payment plan is available for edit.", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

               }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
               gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanEnd, "Exception: Edit payment plan PlanID: " + ex.Message, _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
           }
       }

       private void DisplayWebBrowser(string content,string sFormName)
       {
           try
           {
               ClearGage.frmCGWebBrowser oWeb = new ClearGage.frmCGWebBrowser(ssoHelper);

               //frmCGWebBrowser oWebUI = new frmCGWebBrowser(ssoHelper);
               oWeb.Text = sFormName;
               switch (sFormName)
               {
                   case "Add Payment Plan":
                       {
                           gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageAddPaymentPlan, gloAuditTrail.ActivityType.Open, "Add payment plan click", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                           oWeb.Icon = gloBilling.Properties.Resources.AddPaymentPlan;
                           break;
                       }
                   case "Edit Payment Plan":
                       {
                           gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.Open, "Edit payment plan click", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                           oWeb.Icon = gloBilling.Properties.Resources.EditPaymentPlan; 
                           break;
                       }
                   case "One Time Payment":
                       {
                           gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Open, "One time transaction click", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                           oWeb.Icon = gloBilling.Properties.Resources.OneTimePayment1; 
                           break;
                       }
                   case "Recent Transactions":
                       {
                           gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.Open, "Recent transaction click", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                           oWeb.Icon = gloBilling.Properties.Resources.ViewRecentTransactions; 
                           break;
                       }
               }
               oWeb.LoadContent(content);
               oWeb.ShowDialog();
               oWeb.Dispose();
               FillOnlineActvity();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
               gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageAddPaymentPlan, gloAuditTrail.ActivityType.Open, "Exception in DisplayWebBrowser()" + ex.Message, _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);

           }
       }

       private void tsb_Pat_OA_OneTimePayment_Click(object sender, EventArgs e)
       {
           //lblActivityName.Text = "One Time Payment";
           //try
           //{
           //    DialogResult dgPaymentPlan = DialogResult.No;
           //    if (c1PaymentPlan.Rows.Count > 1)
           //    {
           //        dgPaymentPlan = MessageBox.Show("Payment plan(s) is associated with selected patient. Do you want to associate payment plan for one time payment", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
           //    }
           //    if (dgPaymentPlan == DialogResult.Yes)
           //    {
           //        frmCGPaymentPlanList frmPaymentPlanList = new frmCGPaymentPlanList();
           //        frmPaymentPlanList.PatientID = _nPatientID;
           //        frmPaymentPlanList.PAccountID = _nPAccountId;
           //        frmPaymentPlanList.ssoHelper = ssoHelper;
           //        frmPaymentPlanList.ShowDialog();
           //        string sPaymentPlanID = frmPaymentPlanList.PaymentPlanID;
           //        ClearGage.SSO.Patient oPatient = GetPatientInfo(_nPatientID);
           //        double amount = 10;
           //        string content = ssoHelper.GetOneTimePaymentDialogHtml(oPatient, amount, sPaymentPlanID);
           //        DisplayWebBrowser(content, "One Time Payment");
           //    }
           //    else
           //    {
           //        ClearGage.SSO.Patient oPatient = GetPatientInfo(_nPatientID);
           //        double amount = 10;
           //        string content = ssoHelper.GetOneTimePaymentDialogHtml(oPatient, amount);
           //        DisplayWebBrowser(content, "One Time Payment");
           //    }
           //}
           //catch (Exception ex)
           //{
           //    MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
           //}
       }

       private void tsb_Pat_OA_Subscriptions_Click(object sender, EventArgs e)
       {

       }

       private void tsb_Pat_OA_Transactions_Click(object sender, EventArgs e)
       {
           gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.RecentTransctionBegin, "Recent transaction start", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
           
           try
           {
               ClearGage.SSO.Patient oPatient = GetPatientInfo(_nPatientID);
               ClearGage.SSO.Response.Patient oCGPatient = ssoHelper.GetPatient(oPatient.PatientId);
               string content = ssoHelper.GetRecentTransactionsDialogHtml(oPatient.PatientId);
               DisplayWebBrowser(content, "Recent Transactions");
               gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.RecentTransctionEnd, "Recent transaction start", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

           }
           catch (Exception ex)
           {
               if (ex.Message == "SSO.INVALID_PATIENTID")
               {
                   MessageBox.Show("Selected patient not register with cleargage.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.RecentTransctionEnd, "Exception: Recent transaction: " + ex.Message, _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);

                   return;
               }
               else
               {
                   MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.RecentTransctionEnd, "Exception: Recent transaction: " + ex.Message, _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);

               }
           }
           
           //ClearGage.SSO.Response.Transaction[] transactions = ssoHelper.GetPaymentTransactions(DateTime.Now.AddMonths(-1), DateTime.Now, oPatient.PatientId);
           //c1OnlineActivity.DataSource = transactions;
       }

       private void tsb_Pat_OA_AccountOnFile_Click(object sender, EventArgs e)
       {
           ////lblActivityName.Text = "Recent Transaction";
           //ClearGage.SSO.Patient oPatient = GetPatientInfo(_nPatientID);
           //ClearGage.SSO.Response.Transaction[] transactions = ssoHelper.GetAccountOnFile(oPatient.PatientId);
           ////c1OnlineActivity.DataSource = transactions;

           //string content = ssoHelper.GetAccountOnFileDialogHtml(oPatient);
           //DisplayWebBrowser(content,"Account On File");
          
       }
       private void SetClearGageCallbacks(ClearGage.SSO.SsoHelper ssoHelper)
       {
          // ssoHelper.OneTimePaymentDialogCallback = new ClearGage.SSO.OneTimePaymentDialogResponseHandler(OneTimePaymentDialogCallback);
           ssoHelper.EditPaymentPlanDialogCallback = new ClearGage.SSO.EditPaymentPlanDialogResponseHandler(EditPaymentPlanDialogCallback);
           //ssoHelper.AddPaymentPlanDialogCallback = new ClearGage.SSO.AddPaymentPlanDialogResponseHandler(AddPaymentPlanDialogCallback);
           //ssoHelper.RecentTransactionsDialogCallback = new ClearGage.SSO.RecentTransactionsDialogResponseHandler(ResentTransactionsDialogCallback);
           //ssoHelper.AccountOnFileDialogCallback = new ClearGage.SSO.AccountOnFileResponseHandler(AccountOnFileDialogCallback);
       }
       private void OneTimePaymentDialogCallback(ClearGage.SSO.Response.Transaction[] transactions)
       {
           //FillActvity(transactions,OnlineActivity.OneTimePayment);
           //DisplayObject(transactions);
       }
       private void AccountOnFileDialogCallback(ClearGage.SSO.Response.Transaction[] transactions)
       {
           //FillActvity(transactions, OnlineActivity.Transaction);
           //DisplayObject(transactions);
       }
       private void EditPaymentPlanDialogCallback(ClearGage.SSO.Response.PaymentPlanDetails planDetails)
       {
           FillOnlineActvity(OnlineActivity.All,0,planDetails.PaymentPlan.PlanId);
           //DisplayObject(planDetails);
       }
       private void AddPaymentPlanDialogCallback(ClearGage.SSO.Response.PaymentPlanDetails planDetails)
       {
           //FillActvity(planDetails,OnlineActivity.PaymentPlan);
           //DisplayObject(planDetails);
       }
       private void ResentTransactionsDialogCallback(ClearGage.SSO.Response.Transaction[] transactions)
       {
           //FillActvity(transactions,OnlineActivity.Transaction);
           //DisplayObject(planDetails);
       }
       private void FillActvity(object obj,OnlineActivity onlineActivity)
       {
           try
           {
               switch (onlineActivity)
               {
                   case OnlineActivity.PaymentPlan:
                       ClearGage.SSO.Response.PaymentPlanDetails oPlanDetails = (ClearGage.SSO.Response.PaymentPlanDetails)obj;
                       c1PaymentPlan.DataSource = null;
                       List<ClearGage.SSO.Response.PaymentPlan> lstPaymentPlan = new List<ClearGage.SSO.Response.PaymentPlan>();
                       lstPaymentPlan.Add(oPlanDetails.PaymentPlan);
                       c1PaymentPlan.DataSource = lstPaymentPlan;
                       break;
                   case OnlineActivity.OneTimePayment:
                       //ClearGage.SSO.Response.Transaction[] oOTPTransactions = (ClearGage.SSO.Response.Transaction[])obj;
                       //c1OneTimePayment.DataSource = null;
                       //c1OneTimePayment.DataSource = oTransactions;
                       break;
                   case OnlineActivity.Subscriptions:
                       break;
                   case OnlineActivity.Transaction:
                       //ClearGage.SSO.Response.Transaction[] oRecentTransactions = (ClearGage.SSO.Response.Transaction[])obj;
                       //c1PlanEncounter.DataSource = null;
                       //c1PlanEncounter.DataSource = oRecentTransactions;
                       break;

               }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
           //if (onlineActivity==OnlineActivity.PaymentPlan)
           //{
           //    ClearGage.SSO.Response.PaymentPlanDetails oPlanDetails = (ClearGage.SSO.Response.PaymentPlanDetails)obj;
           //    c1OnlineActivity.DataSource = null;
           //    List<ClearGage.SSO.Response.PaymentPlan> lstPaymentPlan = new List<ClearGage.SSO.Response.PaymentPlan>();
           //    lstPaymentPlan.Add(oPlanDetails.PaymentPlan);
           //    c1OnlineActivity.DataSource = lstPaymentPlan;
           //}
       }

       private void FillOnlineActvity(OnlineActivity ActvityFor=OnlineActivity.All,Int64 nPatientID=0,string sPlanID="")
       {
           try
           {
               gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Fill Online Activity Starts", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
               ClearGage.SSO.Patient oPatient = GetPatientInfo(_nPatientID);
               ClearGage.SSO.Response.PaymentPlan[] oPlans = null;
               ClearGage.SSO.Response.Encounter[] oEncounters=null;
               //ClearGage.SSO.Response.Transaction[] oTransactions = null;
               try
               {
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Fill Online Activity: Check Patient registration Start", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                   ClearGage.SSO.Response.Patient oCGPatient = ssoHelper.GetPatient(oPatient.PatientId);
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Fill Online Activity: Check Patient registration End", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                   switch (ActvityFor)
                   {
                       case OnlineActivity.PaymentPlan:
                           {
                               gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Fill Online Activity: Payment Plan", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                               oPlans = ssoHelper.GetPaymentPlans(oPatient.PatientId);
                               break;
                           }
                       case OnlineActivity.OneTimePayment:
                           break;
                       case OnlineActivity.Subscriptions:
                           break;
                       case OnlineActivity.Transaction:
                           break;
                       case OnlineActivity.Encounter:
                           {
                               gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Fill Online Activity: Encounter", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                               oEncounters = ssoHelper.GetPaymentPlanEncounters(sPlanID);
                               break;
                           }
                       case OnlineActivity.All:
                           {
                               gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Fill Online Activity: Payment Plan and Encounter", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                               oPlans = ssoHelper.GetPaymentPlans(oPatient.PatientId);
                               if (oPlans.Count()>0)
                               {
                                   oEncounters = ssoHelper.GetPaymentPlanEncounters(oPlans[0].PlanId); 
                               }
                               break;
                           }
                       default:
                           break;
                   }
                   
                   //oTransactions = ssoHelper.GetPaymentTransactions(DateTime.Now.AddMonths(-6), DateTime.Now, oPatient.PatientId);
               }
               catch (Exception ex)
               {
                   if (ex.Message == "SSO.INVALID_PATIENTID")
                   {
                       MessageBox.Show("Selected patient not register with cleargage.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.RecentTransctionEnd, "Exception: FillOnlineActivity(): " + ex.Message, _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);

                       return;
                   }
                   else
                   {
                       gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageRecentTransaction, gloAuditTrail.ActivityType.RecentTransctionEnd, "Exception: FillOnlineActivity(): " + ex.ToString(), _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                       MessageBox.Show(ex.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
               }
               if (oPlans != null && oPlans.Length > 0)
               {
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Edit Payment Plan Start", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success,gloAuditTrail.SoftwareComponent.gloPM,true);
                   c1PaymentPlan.DataSource = oPlans;
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Edit Payment Plan End", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
               }
               if (oEncounters != null && oEncounters.Length > 0)
               {
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "plan encounter start", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                   c1PlanEncounter.DataSource = oEncounters;
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "plan encounter End", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
               }
               //foreach (ClearGage.SSO.Response.PaymentPlan item in plans)
               //{

               //}
           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
           }
       }

       private ClearGage.SSO.Patient GetPatientInfo(long nPatientID)
       {
               ClearGage.SSO.Patient oPat=null;
               DataTable dt = null;
               gloPatient.gloPatient oPatient = null;
               try
               {
                   oPat = new ClearGage.SSO.Patient();
                   oPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
                   dt = oPatient.GetPatientDemographics(nPatientID);

                   if (dt != null && dt.Rows.Count > 0)
                   {
                       foreach (DataRow dr in dt.Rows)
                       {
                           oPat.FirstName = Convert.ToString(dr["sFirstName"]);
                           oPat.LastName = Convert.ToString(dr["sLastName"]);
                           oPat.BirthDate = Convert.ToString(dr["dtDOB"]);
                           oPat.Gender = Convert.ToString(dr["sGender"]).Substring(0, 1);
                           oPat.Address1 = Convert.ToString(dr["sAddressLine1"]);
                           oPat.Address2 = Convert.ToString(dr["sAddressLine2"]);
                           oPat.City = Convert.ToString(dr["sCity"]);
                           oPat.State = Convert.ToString(dr["sState"]);
                           oPat.Zip = Convert.ToString(dr["sZip"]);
                           oPat.Ssn = Convert.ToString(dr["nSSN"]);
                           oPat.EmailAddress = Convert.ToString(dr["sEmail"]);
                           oPat.MobilePhone = Convert.ToString(dr["sMobile"]);
                           oPat.Phone = Convert.ToString(dr["sPhone"]);
                           oPat.DriversLicenseNumber = "";
                           oPat.DriversLicenseState = "";
                           oPat.PatientId = Convert.ToString(dt.Rows[0]["sPatientCode"]);
                       }
                   }
               }
               catch (Exception ex)
               {
                   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageURL, gloAuditTrail.ActivityType.EditPaymentPlanEnd, "Exception: GetPatientInfo(): " + ex.Message, _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                   MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
               return oPat;
       }

       private void c1PaymentPlan_MouseDown(object sender, MouseEventArgs e)
       {
           try
           {
               if (c1PaymentPlan.DataSource != null && c1PaymentPlan.Rows.Count > 0)
               {
                   if (c1PaymentPlan.HitTest(e.X, e.Y).Row >= 1)
                   {
                       Int32 tempRow = 0;
                       tempRow = c1PaymentPlan.HitTest(e.X, e.Y).Row;
                       string sPlanID = Convert.ToString(c1PaymentPlan.GetData(tempRow, 1));
                       gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Fetch encounter for Plan: " + sPlanID, _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                       FillOnlineActvity(OnlineActivity.Encounter, 0, sPlanID);
                       gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanBegin, "Fetch encounter for Plan: " + sPlanID, _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                   }
                   else
                   {
                       if (c1PaymentPlan.DataSource == null)
                       {
                           gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageEditPaymentPlan, gloAuditTrail.ActivityType.EditPaymentPlanEnd, "No payment plan is available to fetch encounter details.", _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                           MessageBox.Show("No payment plan is available to fetch encounter details.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       }
                   }
               }
           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageURL, gloAuditTrail.ActivityType.EditPaymentPlanEnd, "Exception: GetPatientInfo(): " + ex.Message, _nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
               MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
       }
       #endregion

       private void btnViewLog_Click(object sender, EventArgs e)
       {
           try
           {
               if (chkDate.Checked == true)
               {
                   tbpgAccountLog.Tag = null;
                   tbPatientFinancial_SelectedIndexChanged(null, null);
               }
               else
               {
                   tbpgAccountLog.Tag = null;
                   tbPatientFinancial_SelectedIndexChanged(null, null);
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
       }

       private void chkDate_CheckedChanged(object sender, EventArgs e)
       {
           if (chkDate.Checked == true)
           {
               dtStartDate.Enabled = true;
               dtEndDate.Enabled = true;
           }
           else
           {
               dtStartDate.Enabled = false;
               dtEndDate.Enabled = false;
           }
       }

      

     

       


        //private void FillChronology()
        //{
        //    gloPatientFinancialViewV2 objPatFinacialView = new gloPatientFinancialViewV2( gloPMGlobal.DatabaseConnectionString, _nPatientID, gloPMGlobal.ClinicID);
        //    DBLayer oDB = new DBLayer(this. gloPMGlobal.DatabaseConnectionString);
        //    DBParameters oParameters = new DBParameters();
        //    DataSet dsChronology = new DataSet();
        //    try
        //    {
        //        objPatFinacialView.GetChronology(oParameters, oDB, out dsChronology);
        //        this.c1FlexGridChronology.DataMember = "Chronology";
        //        this.c1FlexGridChronology.DataSource = dsChronology;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //    finally
        //    {
        //        if (objPatFinacialView != null)
        //        { objPatFinacialView.Dispose(); }
        //    }

        //}


        //private void ts_VoidPayment_Click(object sender, EventArgs e)
        //{
        //    DialogResult dlgRst = DialogResult.None;
        //    gloEOBPaymentPatient ogloPaymentPatient = new gloEOBPaymentPatient( gloPMGlobal.DatabaseConnectionString);
        //    int iVoidCloseDate = 0;
        //    string strVoidTrayName = "";
        //    Int64 nVoidTrayId = 0;
        //    string strVoidTrayCode = "";
        //    Int64 nRetVal = 0;
        //    string strVoidNotes = "";
        //    bool blnIsVoid = false;
        //    bool bIsRefunded = false;
        //    try
        //    {
        //        if (c1FlexGridPmnt.Rows.Count > 1)
        //        {
        //            _nEOBPaymentID = Convert.ToInt64(c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nEOBPaymentID"].Index).ToString());
        //           // ********Check for voided claim************
        //            bIsRefunded = ogloPaymentPatient.IsRefunded(_nEOBPaymentID);
        //            if (bIsRefunded)
        //            {
        //                MessageBox.Show("Payment has been refunded so it may not be voided. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }

        //            blnIsVoid = Convert.ToBoolean(c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["bISVoid"].Index));
        //            if (blnIsVoid)
        //            {
        //                MessageBox.Show("Payment is already voided. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }
        //            //*****************End************************
        //            _strCloseDate = c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nCloseDate"].Index).ToString();
        //            dlgRst = MessageBox.Show("Do you want to void the payment? ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        //            try
        //            {
        //                if (dlgRst == DialogResult.Yes)
        //                {
        //                    frmVoidPayment ofrmVoid = new frmVoidPayment(_nEOBPaymentID);
        //                    ofrmVoid.ShowDialog();
        //                    if (ofrmVoid.oDialogResult)
        //                    {
        //                        iVoidCloseDate = ofrmVoid.VoidCloseDate;
        //                        strVoidTrayName = ofrmVoid.VoidTrayName;
        //                        nVoidTrayId = ofrmVoid.VoidTrayID;
        //                        strVoidTrayCode = ofrmVoid.VoidTrayCode;
        //                        strVoidNotes = ofrmVoid.VoidNotes;

        //                        nRetVal = ogloPaymentPatient.VoidPatientPayment(_nEOBPaymentID, _nPatientID, "", _strCloseDate, strVoidNotes, iVoidCloseDate, nVoidTrayId, strVoidTrayCode, strVoidTrayName);

        //                    }
        //                    ofrmVoid.Dispose();
        //                }
        //                oPatientControl.FillDetails(_nPatientID, gloPatientStripControl.FormName.Billing, 0, false);
        //                tbPatientFinancial_SelectedIndexChanged(sender, e);
        //            }
        //            catch (Exception Ex)
        //            {
        //                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("No existing payment found for void. ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception EX)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), true);
        //    }

        //}

        //private void DesignRefundGrid()
        //{

        //    try
        //    {
        //        #region " Grid Settings "

        //        c1InsuranceRefundLog.Redraw = false;
        //        c1InsuranceRefundLog.Clear();

        //        c1InsuranceRefundLog.Cols.Count = COL_COUNT_REF;
        //        c1InsuranceRefundLog.Rows.Count = 1;
        //        c1InsuranceRefundLog.Rows.Fixed = 1;
        //        c1InsuranceRefundLog.Cols.Fixed = 0;

        //        c1InsuranceRefundLog.AllowEditing = false;
        //        c1InsuranceRefundLog.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
        //        c1InsuranceRefundLog.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

        //        #endregion

        //        #region " Set Headers "

        //        c1InsuranceRefundLog.SetData(0, COL_CLOSEDATE_REF, "Close Date");
        //        c1InsuranceRefundLog.SetData(0, COL_TRAY_REF, "Tray");
        //        c1InsuranceRefundLog.SetData(0, COL_COMPANY_REF, "Refund To");
        //        c1InsuranceRefundLog.SetData(0, COL_CHECK_NO_REF, "Refund Check#");
        //        c1InsuranceRefundLog.SetData(0, COL_REFUND_DATE_REF, "Refund Date");
        //        c1InsuranceRefundLog.SetData(0, COL_REFUND_AMOUNT_REF, "Amount");
        //        c1InsuranceRefundLog.SetData(0, COL_USER_REF, "User");
        //        c1InsuranceRefundLog.SetData(0, COL_NOTE_REF, "Note");
        //        c1InsuranceRefundLog.SetData(0, COL_DATETIME_REF, "Date / Time");
        //        c1InsuranceRefundLog.SetData(0, COL_STATUS_REF, "Status");
        //        c1InsuranceRefundLog.SetData(0, COL_REFUNDID_REF, "nRefundID");
        //        c1InsuranceRefundLog.SetData(0, COL_CLAIMNO_REF, "Claim #");
        //        c1InsuranceRefundLog.SetData(0, COL_PATIENTNAME_REF, "nRefundID");

        //        #endregion

        //        #region " Show/Hide "

        //        c1InsuranceRefundLog.Cols[COL_EOBPAYMENT_ID_REF].Visible = false;
        //        c1InsuranceRefundLog.Cols[COL_COMPANY_ID_REF].Visible = false;
        //        c1InsuranceRefundLog.Cols[COL_PAYMENT_TRAY_ID_REF].Visible = false;
        //        c1InsuranceRefundLog.Cols[COL_USER_ID_REF].Visible = false;
        //        c1InsuranceRefundLog.Cols[COL_DEBIT_AMOUNT_REF].Visible = false;
        //        c1InsuranceRefundLog.Cols[COL_REFUNDID_REF].Visible = false;
        //        c1InsuranceRefundLog.Cols[COL_DATETIME_REF].Visible = false;
        //        c1InsuranceRefundLog.Cols[COL_PATIENTNAME_REF].Visible = false;

        //        #endregion

        //        #region " Width "

        //        c1InsuranceRefundLog.Cols[COL_CLOSEDATE_REF].Width = 75;
        //        c1InsuranceRefundLog.Cols[COL_TRAY_REF].Width = 90;
        //        c1InsuranceRefundLog.Cols[COL_COMPANY_REF].Width = 150;
        //        c1InsuranceRefundLog.Cols[COL_COMPANY_ID_REF].Width = 0;
        //        c1InsuranceRefundLog.Cols[COL_CHECK_NO_REF].Width = 100;
        //        c1InsuranceRefundLog.Cols[COL_REFUND_DATE_REF].Width = 90;
        //        c1InsuranceRefundLog.Cols[COL_REFUND_AMOUNT_REF].Width = 90;
        //        c1InsuranceRefundLog.Cols[COL_USER_REF].Width = 85;
        //        c1InsuranceRefundLog.Cols[COL_NOTE_REF].Width = 210;
        //        c1InsuranceRefundLog.Cols[COL_STATUS_REF].Width = 100;
        //        c1InsuranceRefundLog.Cols[COL_DATETIME_REF].Width = 0;
        //        c1InsuranceRefundLog.Cols[COL_CLAIMNO_REF].Width = 100;
        //        c1InsuranceRefundLog.Cols[COL_PATIENTNAME_REF].Width = 0;

        //        #endregion

        //        #region " Data Type "

        //        c1InsuranceRefundLog.Cols[COL_CLOSEDATE_REF].DataType = typeof(System.String);
        //        c1InsuranceRefundLog.Cols[COL_TRAY_REF].DataType = typeof(System.String);
        //        c1InsuranceRefundLog.Cols[COL_COMPANY_REF].DataType = typeof(System.String);
        //        c1InsuranceRefundLog.Cols[COL_CHECK_NO_REF].DataType = typeof(System.String);

        //        c1InsuranceRefundLog.Cols[COL_REFUND_DATE_REF].DataType = typeof(System.String);
        //        c1InsuranceRefundLog.Cols[COL_NOTE_REF].DataType = typeof(System.String);
        //        c1InsuranceRefundLog.Cols[COL_STATUS_REF].DataType = typeof(System.String);
        //        c1InsuranceRefundLog.Cols[COL_DATETIME_REF].DataType = typeof(System.String);
        //        c1InsuranceRefundLog.Cols[COL_REFUNDID_REF].DataType = typeof(System.Object);
        //        c1InsuranceRefundLog.Cols[COL_CLAIMNO_REF].DataType = typeof(System.String);
        //        c1InsuranceRefundLog.Cols[COL_PATIENTNAME_REF].DataType = typeof(System.Object);

        //        #endregion

        //        #region " Alignment "

        //        c1InsuranceRefundLog.Cols[COL_COMPANY_REF].TextAlign = TextAlignEnum.LeftCenter;
        //        c1InsuranceRefundLog.Cols[COL_CHECK_NO_REF].TextAlign = TextAlignEnum.LeftCenter;

        //        #endregion

        //        #region " Set Styles "

        //        C1.Win.C1FlexGrid.CellStyle csCurrencyStyle = c1InsuranceRefundLog.Styles.Add("cs_CurrencyStyle");
        //        csCurrencyStyle.DataType = typeof(System.Decimal);
        //        csCurrencyStyle.Format = "c";
        //        csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        //        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

        //        c1InsuranceRefundLog.Cols[COL_REFUND_AMOUNT_REF].Style = csCurrencyStyle;
        //        csCurrencyStyle.BackColor = Color.White;
        //        //c1InsuranceRefundLog.Cols[COL_USER].Style = csCurrencyStyle;

        //        //c1InsuranceLog.KeyActionEnter = KeyActionEnum.MoveAcross;
        //        //c1InsuranceLog.VisualStyle = VisualStyle.Custom;
        //        //c1InsuranceLog.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
        //        //c1InsuranceLog.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
        //        //c1InsuranceLog.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

        //        #endregion
        //    }
        //    catch (Exception ex)
        //    { MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //    finally
        //    { c1InsuranceRefundLog.Redraw = true; }
        //}

        //private void DesignTotalRefundGrid()
        //{

        //    try
        //    {
        //        #region " Grid Settings "

        //        c1InsRefundTotal.Redraw = false;
        //        c1InsRefundTotal.Clear();

        //        c1InsRefundTotal.Cols.Count = COL_COUNT_REF;
        //        c1InsRefundTotal.Rows.Count = 1;
        //        c1InsRefundTotal.Rows.Fixed = 1;
        //        c1InsRefundTotal.Cols.Fixed = 0;

        //        c1InsRefundTotal.AllowEditing = false;
        //        c1InsRefundTotal.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
        //        //c1InsRefundTotal.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;
        //        c1InsRefundTotal.AllowSorting = AllowSortingEnum.None;

        //        #endregion


        //        #region " Show/Hide "

        //        c1InsRefundTotal.Cols[COL_EOBPAYMENT_ID_REF].Visible = false;
        //        c1InsRefundTotal.Cols[COL_COMPANY_ID_REF].Visible = false;
        //        c1InsRefundTotal.Cols[COL_PAYMENT_TRAY_ID_REF].Visible = false;
        //        c1InsRefundTotal.Cols[COL_USER_ID_REF].Visible = false;
        //        c1InsRefundTotal.Cols[COL_DEBIT_AMOUNT_REF].Visible = false;
        //        c1InsRefundTotal.Cols[COL_REFUNDID_REF].Visible = false;
        //        c1InsRefundTotal.Cols[COL_DATETIME_REF].Visible = false;
        //        c1InsRefundTotal.Cols[COL_PATIENTNAME_REF].Visible = false;

        //        #endregion

        //        #region " Width "

        //        c1InsRefundTotal.Cols[COL_CLOSEDATE_REF].Width = 75;
        //        c1InsRefundTotal.Cols[COL_TRAY_REF].Width = 90;
        //        c1InsRefundTotal.Cols[COL_COMPANY_REF].Width = 150;
        //        c1InsRefundTotal.Cols[COL_COMPANY_ID_REF].Width = 0;
        //        c1InsRefundTotal.Cols[COL_CHECK_NO_REF].Width = 100;
        //        c1InsRefundTotal.Cols[COL_REFUND_DATE_REF].Width = 90;
        //        c1InsRefundTotal.Cols[COL_REFUND_AMOUNT_REF].Width = 90;
        //        c1InsRefundTotal.Cols[COL_USER_REF].Width = 85;
        //        c1InsRefundTotal.Cols[COL_NOTE_REF].Width = 210;
        //        c1InsRefundTotal.Cols[COL_STATUS_REF].Width = 100;
        //        c1InsRefundTotal.Cols[COL_DATETIME_REF].Width = 0;
        //        c1InsRefundTotal.Cols[COL_CLAIMNO_REF].Width = 100;
        //        c1InsRefundTotal.Cols[COL_PATIENTNAME_REF].Width = 0;

        //        #endregion

        //        #region " Data Type "

        //        c1InsRefundTotal.Cols[COL_CLOSEDATE_REF].DataType = typeof(System.String);
        //        c1InsRefundTotal.Cols[COL_TRAY_REF].DataType = typeof(System.String);
        //        c1InsRefundTotal.Cols[COL_COMPANY_REF].DataType = typeof(System.String);
        //        c1InsRefundTotal.Cols[COL_CHECK_NO_REF].DataType = typeof(System.String);

        //        c1InsRefundTotal.Cols[COL_REFUND_DATE_REF].DataType = typeof(System.String);
        //        c1InsRefundTotal.Cols[COL_NOTE_REF].DataType = typeof(System.String);
        //        c1InsRefundTotal.Cols[COL_STATUS_REF].DataType = typeof(System.String);
        //        c1InsRefundTotal.Cols[COL_DATETIME_REF].DataType = typeof(System.String);
        //        c1InsRefundTotal.Cols[COL_REFUNDID_REF].DataType = typeof(System.Object);
        //        c1InsRefundTotal.Cols[COL_CLAIMNO_REF].DataType = typeof(System.String);
        //        c1InsRefundTotal.Cols[COL_PATIENTNAME_REF].DataType = typeof(System.String);

        //        #endregion

        //        #region " Alignment "

        //        c1InsRefundTotal.Cols[COL_COMPANY_REF].TextAlign = TextAlignEnum.LeftCenter;
        //        c1InsRefundTotal.Cols[COL_CHECK_NO_REF].TextAlign = TextAlignEnum.LeftCenter;

        //        #endregion

        //        #region " Set Styles "

        //        C1.Win.C1FlexGrid.CellStyle csCurrencyStyle = c1InsRefundTotal.Styles.Add("cs_CurrencyStyle");
        //        csCurrencyStyle.DataType = typeof(System.Decimal);
        //        csCurrencyStyle.Format = "c";
        //        csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        //        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
        //        csCurrencyStyle.ForeColor = Color.Blue;

        //        c1InsRefundTotal.Cols[COL_REFUND_AMOUNT_REF].Style = csCurrencyStyle;

        //        #endregion
        //    }
        //    catch (Exception ex)
        //    { MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //    finally
        //    { c1InsRefundTotal.Redraw = true; }
        //}

        //private void DesignReserveGrid()
        //{
        //    try
        //    {
        //        _IsFormLoading = true;
        //        c1Reserve.Redraw = false;
        //        c1Reserve.AllowSorting = AllowSortingEnum.None;

        //        c1Reserve.Clear();
        //        c1Reserve.Cols.Count = COL_COUNT;
        //        c1Reserve.Rows.Count = 1;
        //        c1Reserve.Rows.Fixed = 1;
        //        c1Reserve.Cols.Fixed = 0;

        //        #region " Set Headers "

        //        c1Reserve.SetData(0, COL_EOBPAYMENTID, "EOBPaymentID");
        //        //c1Reserve.SetData(0, COL_EOBID, "EOBID");
        //        //c1Reserve.SetData(0, COL_EOBDTLID, "EOBDetailID");
        //        //c1Reserve.SetData(0, COL_EOBPAYMENTDTLID, "EOBPaymentDetailID");
        //        //c1Reserve.SetData(0, COL_BLTRANSACTIONID, "BillingTransactioID");
        //        //c1Reserve.SetData(0, COL_BLTRANDTLID, "BillingTransactioDetailID");
        //        //c1Reserve.SetData(0, COL_BLTRANLINEID, "BillingTransactioLineID");
        //        //c1Reserve.SetData(0, COL_DOSFROM, "DOSFrom");
        //        //c1Reserve.SetData(0, COL_DOSTO, "DOSTo");
        //        //c1Reserve.SetData(0, COL_PATIENTID, "PatientID");
        //        c1Reserve.SetData(0, COL_SOURCE, "Source");//Patient or Insurance Name
        //        c1Reserve.SetData(0, COL_COMPANYNAME, "Insurance Company");
        //        c1Reserve.SetData(0, COL_ORIGINALPAYMENT, "Original Payment");//Check Number,Date,Amount         
        //        c1Reserve.SetData(0, COL_USERNAME, "User");
        //        c1Reserve.SetData(0, COL_TORESERVES, "To Reserves");//Amount for reserve
        //        c1Reserve.SetData(0, COL_TYPE, "Type");//Copay,Advance,Other
        //        c1Reserve.SetData(0, COL_NOTE, "Note");//Note

        //        c1Reserve.SetData(0, COL_AVAILABLE, "Available");//Available amount
        //        c1Reserve.SetData(0, COL_USERESERVE, "Used");//Used Reserve
        //        c1Reserve.SetData(0, COL_REFUND, "Refund");//Current amount to use from avaiable amount

        //        c1Reserve.SetData(0, COL_PAYMODE, "Payment Mode");
        //        //c1Reserve.SetData(0, COL_REFEOBPAYID, "Ref.EOBID");
        //        //c1Reserve.SetData(0, COL_REFEOBPAYDTLID, "Ref.EOBDetailID");
        //        //c1Reserve.SetData(0, COL_ACCOUNTID, "AccountID");
        //        //c1Reserve.SetData(0, COL_ACCOUNTTYPE, "Account Type");
        //        //c1Reserve.SetData(0, COL_MSTACCOUNTID, "Mst.AccountID");
        //        //c1Reserve.SetData(0, COL_MSTACCOUNTTYPE, "Mst.AccountType");
        //        //c1Reserve.SetData(0, COL_RES_EOBPAYID, "ReserveRefPayID");
        //        //c1Reserve.SetData(0, COL_RES_EOBPAYDTLID, "ReserveRefPayDtlID");
        //        c1Reserve.SetData(0, COL_PAYMENTCLOSEDATE, "Close Date");
        //        c1Reserve.SetData(0, COL_PAYMENTMODE, "sPaymentMode");
        //        c1Reserve.SetData(0, COL_PAYMENTMODENO, "sPaymentNo");
        //        c1Reserve.SetData(0, COL_ASSO_PATIENTID, "PatientID");
        //        c1Reserve.SetData(0, COL_ASSO_PATIENTNAME, "Patient");
        //        c1Reserve.SetData(0, COL_ASSO_MSTTRANSACTIONID, "nTransactionID");
        //        c1Reserve.SetData(0, COL_ASSO_TRACKTRANSACTIONID, "nTrackTrnID");
        //        c1Reserve.SetData(0, COL_ASSO_CLAIMNO, "Claim #");

        //        #endregion

        //        #region " Show/Hide "

        //        c1Reserve.Cols[COL_SOURCE].Visible = true;
        //        c1Reserve.Cols[COL_ORIGINALPAYMENT].Visible = true;
        //        c1Reserve.Cols[COL_TORESERVES].Visible = true;
        //        //c1Reserve.Cols[COL_TYPE].Visible = true;
        //        //c1Reserve.Cols[COL_NOTE].Visible = true;
        //        //c1Reserve.Cols[COL_AVAILABLE].Visible = true;
        //        //c1Reserve.Cols[COL_REFUND].Visible = true;

        //        c1Reserve.Cols[COL_EOBPAYMENTID].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_EOBID].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_EOBDTLID].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_EOBPAYMENTDTLID].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_BLTRANSACTIONID].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_BLTRANDTLID].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_BLTRANLINEID].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_DOSFROM].Visible = false;// 50;
        //        //c1Reserve.Cols[COL_DOSTO].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_PATIENTID].Visible = false;// 0;
        //        // c1Reserve.Cols[COL_SOURCE].Visible = true;// 100;
        //        c1Reserve.Cols[COL_COMPANYNAME].Visible = true;
        //        //c1Reserve.Cols[COL_ORIGINALPAYMENT].Visible = true;// 100;                
        //        c1Reserve.Cols[COL_USERNAME].Visible = true;// 100;
        //        c1Reserve.Cols[COL_TYPE].Visible = false;// 100;
        //        c1Reserve.Cols[COL_NOTE].Visible = true;// 100;
        //        c1Reserve.Cols[COL_AVAILABLE].Visible = true;// 100;
        //        c1Reserve.Cols[COL_REFUND].Visible = false;// 100;
        //        c1Reserve.Cols[COL_PAYMODE].Visible = false;// 100;
        //        //c1Reserve.Cols[COL_REFEOBPAYID].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_REFEOBPAYDTLID].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_ACCOUNTID].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_ACCOUNTTYPE].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_MSTACCOUNTID].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_MSTACCOUNTTYPE].Visible = false;// 0;
        //        //c1Reserve.Cols[COL_USERESERVE].Visible = false;
        //        //c1Reserve.Cols[COL_RES_EOBPAYID].Visible = false;
        //        //c1Reserve.Cols[COL_RES_EOBPAYDTLID].Visible = false;
        //        c1Reserve.Cols[COL_PAYMENTCLOSEDATE].Visible = true;
        //        c1Reserve.Cols[COL_PAYMENTMODE].Visible = false;
        //        c1Reserve.Cols[COL_PAYMENTMODENO].Visible = false;
        //        c1Reserve.Cols[COL_ASSO_PATIENTID].Visible = false;
        //        c1Reserve.Cols[COL_ASSO_PATIENTNAME].Visible = false;
        //        c1Reserve.Cols[COL_ASSO_MSTTRANSACTIONID].Visible = false;
        //        c1Reserve.Cols[COL_ASSO_TRACKTRANSACTIONID].Visible = false;
        //        c1Reserve.Cols[COL_ASSO_CLAIMNO].Visible = true;

        //        #endregion

        //        #region " Width "

        //        bool _designWidth = false;

        //        if (_designWidth == false)
        //        {

        //            c1Reserve.Cols[COL_EOBPAYMENTID].Width = 0;
        //            //c1Reserve.Cols[COL_EOBID].Width = 0;
        //            //c1Reserve.Cols[COL_EOBDTLID].Width = 0;
        //            //c1Reserve.Cols[COL_EOBPAYMENTDTLID].Width = 0;
        //            //c1Reserve.Cols[COL_BLTRANSACTIONID].Width = 0;
        //            //c1Reserve.Cols[COL_BLTRANDTLID].Width = 0;
        //            //c1Reserve.Cols[COL_BLTRANLINEID].Width = 0;
        //            //c1Reserve.Cols[COL_DOSFROM].Width = 50;
        //            //c1Reserve.Cols[COL_DOSTO].Width = 0;
        //            //c1Reserve.Cols[COL_PATIENTID].Width = 0;
        //            c1Reserve.Cols[COL_SOURCE].Width = 0;
        //            c1Reserve.Cols[COL_COMPANYNAME].Width = 140;
        //            c1Reserve.Cols[COL_ORIGINALPAYMENT].Width = 290;
        //            c1Reserve.Cols[COL_USERNAME].Width = 120;
        //            c1Reserve.Cols[COL_TORESERVES].Width = 100;
        //            c1Reserve.Cols[COL_TYPE].Width = 0;
        //            c1Reserve.Cols[COL_NOTE].Width = 220;
        //            c1Reserve.Cols[COL_AVAILABLE].Width = 100;
        //            c1Reserve.Cols[COL_REFUND].Width = 0;
        //            c1Reserve.Cols[COL_PAYMODE].Width = 0;
        //            //c1Reserve.Cols[COL_REFEOBPAYID].Width = 0;
        //            //c1Reserve.Cols[COL_REFEOBPAYDTLID].Width = 0;
        //            //c1Reserve.Cols[COL_ACCOUNTID].Width = 0;
        //            //c1Reserve.Cols[COL_ACCOUNTTYPE].Width = 0;
        //            //c1Reserve.Cols[COL_MSTACCOUNTID].Width = 0;
        //            //c1Reserve.Cols[COL_MSTACCOUNTTYPE].Width = 0;
        //            //c1Reserve.Cols[COL_USERESERVE].Width = 0;
        //            //c1Reserve.Cols[COL_RES_EOBPAYID].Width = 0;
        //            //c1Reserve.Cols[COL_RES_EOBPAYDTLID].Width = 0;
        //            c1Reserve.Cols[COL_PAYMENTCLOSEDATE].Width = 80;
        //            c1Reserve.Cols[COL_PAYMENTMODE].Width = 0;
        //            c1Reserve.Cols[COL_PAYMENTMODENO].Width = 0;
        //            c1Reserve.Cols[COL_ASSO_PATIENTID].Width = 0;
        //            c1Reserve.Cols[COL_ASSO_PATIENTNAME].Width = 0;
        //            c1Reserve.Cols[COL_ASSO_MSTTRANSACTIONID].Width = 0;
        //            c1Reserve.Cols[COL_ASSO_TRACKTRANSACTIONID].Width = 0;
        //            c1Reserve.Cols[COL_ASSO_CLAIMNO].Width = 100;
        //        }

        //        #endregion

        //        #region " Data Type "

        //        c1Reserve.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_EOBID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_EOBDTLID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_EOBPAYMENTDTLID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_BLTRANSACTIONID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_BLTRANDTLID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_BLTRANLINEID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_DOSFROM].DataType = typeof(System.String);
        //        //c1Reserve.Cols[COL_DOSTO].DataType = typeof(System.String);
        //        //c1Reserve.Cols[COL_PATIENTID].DataType = typeof(System.Int64);
        //        c1Reserve.Cols[COL_SOURCE].DataType = typeof(System.String);
        //        c1Reserve.Cols[COL_COMPANYNAME].DataType = typeof(System.String);
        //        c1Reserve.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);
        //        c1Reserve.Cols[COL_USERNAME].DataType = typeof(System.String);
        //        c1Reserve.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
        //        c1Reserve.Cols[COL_TYPE].DataType = typeof(System.String);
        //        c1Reserve.Cols[COL_NOTE].DataType = typeof(System.String);
        //        c1Reserve.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);
        //        c1Reserve.Cols[COL_REFUND].DataType = typeof(System.Decimal);
        //        c1Reserve.Cols[COL_USERESERVE].DataType = typeof(System.Decimal);
        //        c1Reserve.Cols[COL_PAYMODE].DataType = typeof(System.Int32);
        //        //c1Reserve.Cols[COL_REFEOBPAYID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_REFEOBPAYDTLID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_ACCOUNTID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_ACCOUNTTYPE].DataType = typeof(System.Int32);
        //        //c1Reserve.Cols[COL_MSTACCOUNTID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_MSTACCOUNTTYPE].DataType = typeof(System.Int32);
        //        //c1Reserve.Cols[COL_RES_EOBPAYID].DataType = typeof(System.Int64);
        //        //c1Reserve.Cols[COL_RES_EOBPAYDTLID].DataType = typeof(System.Int64);
        //        c1Reserve.Cols[COL_PAYMENTCLOSEDATE].DataType = typeof(System.String);
        //        c1Reserve.Cols[COL_PAYMENTMODE].DataType = typeof(System.String);
        //        c1Reserve.Cols[COL_PAYMENTMODENO].DataType = typeof(System.String);
        //        c1Reserve.Cols[COL_ASSO_PATIENTID].DataType = typeof(System.Int64);
        //        c1Reserve.Cols[COL_ASSO_PATIENTNAME].DataType = typeof(System.String);
        //        c1Reserve.Cols[COL_ASSO_MSTTRANSACTIONID].DataType = typeof(System.Int64);
        //        c1Reserve.Cols[COL_ASSO_TRACKTRANSACTIONID].DataType = typeof(System.Int64);
        //        c1Reserve.Cols[COL_ASSO_CLAIMNO].DataType = typeof(System.String);

        //        #endregion

        //        #region " Alignment "

        //        c1Reserve.Cols[COL_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1Reserve.Cols[COL_EOBID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1Reserve.Cols[COL_EOBDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1Reserve.Cols[COL_EOBPAYMENTDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1Reserve.Cols[COL_BLTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1Reserve.Cols[COL_BLTRANDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1Reserve.Cols[COL_BLTRANLINEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1Reserve.Cols[COL_DOSFROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1Reserve.Cols[COL_DOSTO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1Reserve.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_COMPANYNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_ORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_USERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_TORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_NOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_AVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        c1Reserve.Cols[COL_REFUND].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_USERESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_PAYMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1Reserve.Cols[COL_REFEOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1Reserve.Cols[COL_REFEOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1Reserve.Cols[COL_ACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1Reserve.Cols[COL_ACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1Reserve.Cols[COL_MSTACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1Reserve.Cols[COL_MSTACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1Reserve.Cols[COL_RES_EOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1Reserve.Cols[COL_RES_EOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        c1Reserve.Cols[COL_PAYMENTCLOSEDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_PAYMENTMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        c1Reserve.Cols[COL_PAYMENTMODENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        c1Reserve.Cols[COL_ASSO_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_ASSO_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_ASSO_MSTTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_ASSO_TRACKTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1Reserve.Cols[COL_ASSO_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

        //        #endregion

        //        #region " Set Styles "

        //        C1.Win.C1FlexGrid.CellStyle csCurrencyStyle = c1Reserve.Styles.Add("cs_CurrencyStyle");
        //        csCurrencyStyle.DataType = typeof(System.Decimal);
        //        csCurrencyStyle.Format = "c";
        //        //csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        //        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

        //        C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle = c1Reserve.Styles.Add("cs_EditableCurrencyStyle");
        //        csEditableCurrencyStyle.DataType = typeof(System.Decimal);
        //        csEditableCurrencyStyle.Format = "c";
        //        //csEditableCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        //        //csEditableCurrencyStyle.BackColor = Color.White;

        //        c1Reserve.Cols[COL_TORESERVES].Style = csCurrencyStyle;
        //        c1Reserve.Cols[COL_AVAILABLE].Style = csCurrencyStyle;
        //        c1Reserve.Cols[COL_REFUND].Style = csCurrencyStyle;
        //        c1Reserve.Cols[COL_USERESERVE].Style = csCurrencyStyle;

        //        #endregion

        //        c1Reserve.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
        //        c1Reserve.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

        //        #region " Allow Editing "

        //        c1Reserve.AllowEditing = false;

        //        c1Reserve.Cols[COL_EOBPAYMENTID].AllowEditing = false;
        //        //c1Reserve.Cols[COL_EOBID].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_EOBDTLID].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_EOBPAYMENTDTLID].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_BLTRANSACTIONID].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_BLTRANDTLID].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_BLTRANLINEID].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_DOSFROM].AllowEditing = false;//50;
        //        //c1Reserve.Cols[COL_DOSTO].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_PATIENTID].AllowEditing = false;//0;
        //        c1Reserve.Cols[COL_SOURCE].AllowEditing = false;//100;
        //        c1Reserve.Cols[COL_ORIGINALPAYMENT].AllowEditing = false;//100;
        //        c1Reserve.Cols[COL_USERNAME].AllowEditing = false;//100;
        //        c1Reserve.Cols[COL_TORESERVES].AllowEditing = false;//100;
        //        c1Reserve.Cols[COL_TYPE].AllowEditing = false;//100;
        //        c1Reserve.Cols[COL_NOTE].AllowEditing = false;//100;
        //        c1Reserve.Cols[COL_AVAILABLE].AllowEditing = false;//100;
        //        c1Reserve.Cols[COL_REFUND].AllowEditing = false;//100;
        //        c1Reserve.Cols[COL_USERESERVE].AllowEditing = false;//100;
        //        c1Reserve.Cols[COL_PAYMODE].AllowEditing = false;//100;
        //        //c1Reserve.Cols[COL_REFEOBPAYID].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_REFEOBPAYDTLID].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_ACCOUNTID].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_ACCOUNTTYPE].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_MSTACCOUNTID].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_MSTACCOUNTTYPE].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_RES_EOBPAYID].AllowEditing = false;//0;
        //        //c1Reserve.Cols[COL_RES_EOBPAYDTLID].AllowEditing = false;//0;
        //        c1Reserve.Cols[COL_PAYMENTCLOSEDATE].AllowEditing = false;//0;
        //        c1Reserve.Cols[COL_PAYMENTMODE].AllowEditing = false;//0;
        //        c1Reserve.Cols[COL_PAYMENTMODENO].AllowEditing = false;//0;
        //        c1Reserve.Cols[COL_ASSO_PATIENTID].AllowEditing = false;
        //        c1Reserve.Cols[COL_ASSO_PATIENTNAME].AllowEditing = false;
        //        c1Reserve.Cols[COL_ASSO_MSTTRANSACTIONID].AllowEditing = false;
        //        c1Reserve.Cols[COL_ASSO_TRACKTRANSACTIONID].AllowEditing = false;
        //        c1Reserve.Cols[COL_ASSO_CLAIMNO].AllowEditing = false;

        //        #endregion

        //        //c1Reserve.VisualStyle = VisualStyle.Office2007Blue;
        //        //c1Reserve.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
        //        //c1Reserve.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
        //        //c1Reserve.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);
        //        c1Reserve.ShowCellLabels = false;

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        ex = null;
        //    }
        //    finally
        //    { _IsFormLoading = false; c1Reserve.Redraw = true; }
        //}

        //private void DesignTotalReserveGrid()
        //{
        //    try
        //    {

        //        c1InsReserveTotal.AllowSorting = AllowSortingEnum.None;

        //        c1InsReserveTotal.Clear();
        //        c1InsReserveTotal.Cols.Count = COL_COUNT;
        //        c1InsReserveTotal.Rows.Count = 1;
        //        c1InsReserveTotal.Rows.Fixed = 1;
        //        c1InsReserveTotal.Cols.Fixed = 0;


        //        #region " Show/Hide "

        //        //c1InsReserveTotal.Cols[COL_SOURCE].Visible = true;
        //        //c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].Visible = true;
        //        //c1InsReserveTotal.Cols[COL_TORESERVES].Visible = true;
        //        //c1InsReserveTotal.Cols[COL_TYPE].Visible = true;
        //        //c1InsReserveTotal.Cols[COL_NOTE].Visible = true;
        //        //c1InsReserveTotal.Cols[COL_AVAILABLE].Visible = true;
        //        //c1InsReserveTotal.Cols[COL_REFUND].Visible = true;

        //        c1InsReserveTotal.Cols[COL_EOBPAYMENTID].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_EOBID].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_EOBDTLID].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_EOBPAYMENTDTLID].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_BLTRANSACTIONID].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_BLTRANDTLID].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_BLTRANLINEID].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_DOSFROM].Visible = false;// 50;
        //        //c1InsReserveTotal.Cols[COL_DOSTO].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_PATIENTID].Visible = false;// 0;
        //        c1InsReserveTotal.Cols[COL_SOURCE].Visible = true;// 100;
        //        c1InsReserveTotal.Cols[COL_COMPANYNAME].Visible = true;
        //        c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].Visible = true;// 100;
        //        c1InsReserveTotal.Cols[COL_USERNAME].Visible = true;// 100;
        //        c1InsReserveTotal.Cols[COL_TORESERVES].Visible = true;// 100;
        //        c1InsReserveTotal.Cols[COL_TYPE].Visible = false;// 100;
        //        c1InsReserveTotal.Cols[COL_NOTE].Visible = true;// 100;
        //        c1InsReserveTotal.Cols[COL_AVAILABLE].Visible = true;// 100;
        //        c1InsReserveTotal.Cols[COL_REFUND].Visible = false;// 100;
        //        c1InsReserveTotal.Cols[COL_PAYMODE].Visible = false;// 100;
        //        //c1InsReserveTotal.Cols[COL_REFEOBPAYID].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_REFEOBPAYDTLID].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_ACCOUNTID].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_ACCOUNTTYPE].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_MSTACCOUNTID].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_MSTACCOUNTTYPE].Visible = false;// 0;
        //        //c1InsReserveTotal.Cols[COL_USERESERVE].Visible = false;
        //        //c1InsReserveTotal.Cols[COL_RES_EOBPAYID].Visible = false;
        //        //c1InsReserveTotal.Cols[COL_RES_EOBPAYDTLID].Visible = false;
        //        c1InsReserveTotal.Cols[COL_PAYMENTCLOSEDATE].Visible = true;
        //        c1InsReserveTotal.Cols[COL_PAYMENTMODE].Visible = false;
        //        c1InsReserveTotal.Cols[COL_PAYMENTMODENO].Visible = false;
        //        c1InsReserveTotal.Cols[COL_ASSO_PATIENTID].Visible = false;
        //        c1InsReserveTotal.Cols[COL_ASSO_PATIENTNAME].Visible = false;
        //        c1InsReserveTotal.Cols[COL_ASSO_MSTTRANSACTIONID].Visible = false;
        //        c1InsReserveTotal.Cols[COL_ASSO_TRACKTRANSACTIONID].Visible = false;
        //        c1InsReserveTotal.Cols[COL_ASSO_CLAIMNO].Visible = true;


        //        #endregion

        //        #region " Width "

        //        bool _designWidth = false;

        //        if (_designWidth == false)
        //        {

        //            c1InsReserveTotal.Cols[COL_EOBPAYMENTID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_EOBID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_EOBDTLID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_EOBPAYMENTDTLID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_BLTRANSACTIONID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_BLTRANDTLID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_BLTRANLINEID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_DOSFROM].Width = 50;
        //            //c1InsReserveTotal.Cols[COL_DOSTO].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_PATIENTID].Width = 0;
        //            c1InsReserveTotal.Cols[COL_SOURCE].Width = 0;
        //            c1InsReserveTotal.Cols[COL_COMPANYNAME].Width = 140;
        //            c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].Width = 290;
        //            c1InsReserveTotal.Cols[COL_USERNAME].Width = 150;
        //            c1InsReserveTotal.Cols[COL_TORESERVES].Width = 80;
        //            c1InsReserveTotal.Cols[COL_TYPE].Width = 0;
        //            c1InsReserveTotal.Cols[COL_NOTE].Width = 280;
        //            c1InsReserveTotal.Cols[COL_AVAILABLE].Width = 75;
        //            c1InsReserveTotal.Cols[COL_REFUND].Width = 0;
        //            c1InsReserveTotal.Cols[COL_PAYMODE].Width = 100;
        //            //c1InsReserveTotal.Cols[COL_REFEOBPAYID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_REFEOBPAYDTLID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_ACCOUNTID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_ACCOUNTTYPE].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_MSTACCOUNTID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_MSTACCOUNTTYPE].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_USERESERVE].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_RES_EOBPAYID].Width = 0;
        //            //c1InsReserveTotal.Cols[COL_RES_EOBPAYDTLID].Width = 0;
        //            c1InsReserveTotal.Cols[COL_PAYMENTCLOSEDATE].Width = 80;
        //            c1InsReserveTotal.Cols[COL_PAYMENTMODE].Width = 0;
        //            c1InsReserveTotal.Cols[COL_PAYMENTMODENO].Width = 0;
        //            c1InsReserveTotal.Cols[COL_ASSO_PATIENTID].Width = 0;
        //            c1InsReserveTotal.Cols[COL_ASSO_PATIENTNAME].Width = 0;
        //            c1InsReserveTotal.Cols[COL_ASSO_MSTTRANSACTIONID].Width = 0;
        //            c1InsReserveTotal.Cols[COL_ASSO_TRACKTRANSACTIONID].Width = 0;
        //            c1InsReserveTotal.Cols[COL_ASSO_CLAIMNO].Width = 100;
        //        }

        //        #endregion

        //        #region " Data Type "

        //        c1InsReserveTotal.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_EOBID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_EOBDTLID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_EOBPAYMENTDTLID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_BLTRANSACTIONID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_BLTRANDTLID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_BLTRANLINEID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_DOSFROM].DataType = typeof(System.String);
        //        //c1InsReserveTotal.Cols[COL_DOSTO].DataType = typeof(System.String);
        //        //c1InsReserveTotal.Cols[COL_PATIENTID].DataType = typeof(System.Int64);
        //        c1InsReserveTotal.Cols[COL_SOURCE].DataType = typeof(System.String);
        //        c1InsReserveTotal.Cols[COL_COMPANYNAME].DataType = typeof(System.String);
        //        c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);
        //        c1InsReserveTotal.Cols[COL_USERNAME].DataType = typeof(System.String);
        //        c1InsReserveTotal.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
        //        c1InsReserveTotal.Cols[COL_TYPE].DataType = typeof(System.String);
        //        c1InsReserveTotal.Cols[COL_NOTE].DataType = typeof(System.String);
        //        c1InsReserveTotal.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);
        //        c1InsReserveTotal.Cols[COL_REFUND].DataType = typeof(System.Decimal);
        //        c1InsReserveTotal.Cols[COL_USERESERVE].DataType = typeof(System.Decimal);
        //        c1InsReserveTotal.Cols[COL_PAYMODE].DataType = typeof(System.Int32);
        //        //c1InsReserveTotal.Cols[COL_REFEOBPAYID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_REFEOBPAYDTLID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_ACCOUNTID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_ACCOUNTTYPE].DataType = typeof(System.Int32);
        //        //c1InsReserveTotal.Cols[COL_MSTACCOUNTID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_MSTACCOUNTTYPE].DataType = typeof(System.Int32);
        //        //c1InsReserveTotal.Cols[COL_RES_EOBPAYID].DataType = typeof(System.Int64);
        //        //c1InsReserveTotal.Cols[COL_RES_EOBPAYDTLID].DataType = typeof(System.Int64);
        //        c1InsReserveTotal.Cols[COL_PAYMENTCLOSEDATE].DataType = typeof(System.String);
        //        c1InsReserveTotal.Cols[COL_PAYMENTMODE].DataType = typeof(System.String);
        //        c1InsReserveTotal.Cols[COL_PAYMENTMODENO].DataType = typeof(System.String);
        //        c1InsReserveTotal.Cols[COL_ASSO_PATIENTID].DataType = typeof(System.Int64);
        //        c1InsReserveTotal.Cols[COL_ASSO_PATIENTNAME].DataType = typeof(System.String);
        //        c1InsReserveTotal.Cols[COL_ASSO_MSTTRANSACTIONID].DataType = typeof(System.Int64);
        //        c1InsReserveTotal.Cols[COL_ASSO_TRACKTRANSACTIONID].DataType = typeof(System.Int64);
        //        c1InsReserveTotal.Cols[COL_ASSO_CLAIMNO].DataType = typeof(System.String);

        //        #endregion

        //        #region " Alignment "

        //        c1InsReserveTotal.Cols[COL_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1InsReserveTotal.Cols[COL_EOBID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1InsReserveTotal.Cols[COL_EOBDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1InsReserveTotal.Cols[COL_EOBPAYMENTDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1InsReserveTotal.Cols[COL_BLTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1InsReserveTotal.Cols[COL_BLTRANDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1InsReserveTotal.Cols[COL_BLTRANLINEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1InsReserveTotal.Cols[COL_DOSFROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1InsReserveTotal.Cols[COL_DOSTO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1InsReserveTotal.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_COMPANYNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_USERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_TORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_NOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_AVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        c1InsReserveTotal.Cols[COL_REFUND].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_USERESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_PAYMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1InsReserveTotal.Cols[COL_REFEOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1InsReserveTotal.Cols[COL_REFEOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1InsReserveTotal.Cols[COL_ACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        //c1InsReserveTotal.Cols[COL_ACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1InsReserveTotal.Cols[COL_MSTACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1InsReserveTotal.Cols[COL_MSTACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1InsReserveTotal.Cols[COL_RES_EOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        //c1InsReserveTotal.Cols[COL_RES_EOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        c1InsReserveTotal.Cols[COL_PAYMENTCLOSEDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_PAYMENTMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        c1InsReserveTotal.Cols[COL_PAYMENTMODENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
        //        c1InsReserveTotal.Cols[COL_ASSO_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_ASSO_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_ASSO_MSTTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_ASSO_TRACKTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        //        c1InsReserveTotal.Cols[COL_ASSO_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

        //        #endregion

        //        #region " Set Styles "

        //        C1.Win.C1FlexGrid.CellStyle csCurrencyStyle = c1InsReserveTotal.Styles.Add("cs_CurrencyStyle");
        //        csCurrencyStyle.DataType = typeof(System.Decimal);
        //        csCurrencyStyle.Format = "c";
        //        csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        //        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
        //        csCurrencyStyle.ForeColor = Color.Blue;

        //        //C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle = c1InsReserveTotal.Styles.Add("cs_EditableCurrencyStyle");
        //        //csEditableCurrencyStyle.DataType = typeof(System.Decimal);
        //        //csEditableCurrencyStyle.Format = "c";
        //        //csEditableCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        //        //csEditableCurrencyStyle.BackColor = Color.White;


        //        c1InsReserveTotal.Cols[COL_TORESERVES].Style = csCurrencyStyle;
        //        c1InsReserveTotal.Cols[COL_AVAILABLE].Style = csCurrencyStyle;
        //        c1InsReserveTotal.Cols[COL_REFUND].Style = csCurrencyStyle;
        //        c1InsReserveTotal.Cols[COL_USERESERVE].Style = csCurrencyStyle;

        //        #endregion

        //        c1InsReserveTotal.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
        //        //c1InsReserveTotal.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

        //        #region " Allow Editing "

        //        c1InsReserveTotal.AllowEditing = false;

        //        c1InsReserveTotal.Cols[COL_EOBPAYMENTID].AllowEditing = false;
        //        //c1InsReserveTotal.Cols[COL_EOBID].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_EOBDTLID].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_EOBPAYMENTDTLID].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_BLTRANSACTIONID].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_BLTRANDTLID].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_BLTRANLINEID].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_DOSFROM].AllowEditing = false;//50;
        //        //c1InsReserveTotal.Cols[COL_DOSTO].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_PATIENTID].AllowEditing = false;//0;
        //        c1InsReserveTotal.Cols[COL_SOURCE].AllowEditing = false;//100;
        //        c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].AllowEditing = false;//100;
        //        c1InsReserveTotal.Cols[COL_USERNAME].AllowEditing = false;//100;
        //        c1InsReserveTotal.Cols[COL_TORESERVES].AllowEditing = false;//100;
        //        c1InsReserveTotal.Cols[COL_TYPE].AllowEditing = false;//100;
        //        c1InsReserveTotal.Cols[COL_NOTE].AllowEditing = false;//100;
        //        c1InsReserveTotal.Cols[COL_AVAILABLE].AllowEditing = false;//100;
        //        c1InsReserveTotal.Cols[COL_REFUND].AllowEditing = false;//100;
        //        c1InsReserveTotal.Cols[COL_USERESERVE].AllowEditing = false;//100;
        //        c1InsReserveTotal.Cols[COL_PAYMODE].AllowEditing = false;//100;
        //        //c1InsReserveTotal.Cols[COL_REFEOBPAYID].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_REFEOBPAYDTLID].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_ACCOUNTID].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_ACCOUNTTYPE].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_MSTACCOUNTID].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_MSTACCOUNTTYPE].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_RES_EOBPAYID].AllowEditing = false;//0;
        //        //c1InsReserveTotal.Cols[COL_RES_EOBPAYDTLID].AllowEditing = false;//0;
        //        c1InsReserveTotal.Cols[COL_PAYMENTCLOSEDATE].AllowEditing = false;//0;
        //        c1InsReserveTotal.Cols[COL_PAYMENTMODE].AllowEditing = false;//0;
        //        c1InsReserveTotal.Cols[COL_PAYMENTMODENO].AllowEditing = false;//0;
        //        c1InsReserveTotal.Cols[COL_ASSO_PATIENTID].AllowEditing = false;
        //        c1InsReserveTotal.Cols[COL_ASSO_PATIENTNAME].AllowEditing = false;
        //        c1InsReserveTotal.Cols[COL_ASSO_MSTTRANSACTIONID].AllowEditing = false;
        //        c1InsReserveTotal.Cols[COL_ASSO_TRACKTRANSACTIONID].AllowEditing = false;
        //        c1InsReserveTotal.Cols[COL_ASSO_CLAIMNO].AllowEditing = false;


        //        #endregion

        //        c1InsReserveTotal.ShowCellLabels = false;

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        ex = null;
        //    }
        //    finally
        //    { _IsFormLoading = false; c1InsReserveTotal.Redraw = true; }
        //}


        //private void FillReserves()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    //DataTable _dtReserves = new DataTable();
        //    DataSet dsReserves = new DataSet();
        //    try
        //    {

        //        //DesignPaymentGrid(c1Reserve);

        //        // _IsFormLoading = true;

        //        oParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
        //        oParameters.Add("@nClinicID", gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//NUMERIC(18,0)
        //        //oParameters.Add("@nEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
        //        //oParameters.Add("@nEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0)

        //        oDB.Connect(false);
        //        dsReserves = this.fillgridData("Patient_Financial_View_Reserve", oParameters, oDB);
        //        //oDB.Retrive("BL_SELECT_PaymentTransaction_UseReserve", oParameters, out _dtReserves);
        //        oDB.Disconnect();

        //        if (dsReserves != null && dsReserves.Tables[0].Rows.Count > 0)
        //        {
        //            // int _rowIndex = 0;

        //            //for (int iResCount = 0; iResCount < dsReserves.Tables[0].Rows.Count; i++)
        //            //{
        //                dsReserves.Tables[0].TableName = "Reserves";
        //                this.c1FlexGridAvailResrv.DataMember = "Reserves";
        //                this.c1FlexGridAvailResrv.DataSource = dsReserves;

        //            //}
        //        }


        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //    finally
        //    {
        //        if (oDB != null)
        //            oDB.Dispose();
        //        if (oParameters != null)
        //            oParameters.Dispose();
        //    }
        //}


        //Bind the Patient Refund
        //private void DesignRefundgrid(decimal TotalRefund)
        //{
        //    try
        //    {
        //        // gloC1FlexStyle.Style(c1PatientRefund, true );
        //        c1PatientRefund.ShowCellLabels = false;
        //        #region " Set Header "
        //        c1PatientRefund.Cols["nRefundID"].Caption = "RefundID";
        //        c1PatientRefund.Cols["sRefundTo"].Caption = "To";
        //        c1PatientRefund.Cols["nCloseDate"].Caption = "Close Date";
        //        c1PatientRefund.Cols["Tray"].Caption = "Tray";
        //        c1PatientRefund.Cols["nRefundDate"].Caption = "Refund Date";
        //        c1PatientRefund.Cols["Amount"].Caption = "Amount";
        //        c1PatientRefund.Cols["sNoteDescription"].Caption = "Note";
        //        c1PatientRefund.Cols["sUserName"].Caption = "User";
        //        c1PatientRefund.Cols["dtCreatedDateTime"].Caption = "Date/Time";
        //        c1PatientRefund.Cols["Status"].Caption = "Status";

        //        #endregion

        //        int _nWidth = 0;
        //        _nWidth = 976;//Convert.ToInt32( c1QueuedClaims.Width);
        //        c1PatientRefund.Cols["nRefundID"].Width = 0;
        //        c1PatientRefund.Cols["nRefundID"].Visible = false;
        //        c1PatientRefund.Cols["nCloseDate"].Width = Convert.ToInt32(_nWidth * 0.10);
        //        c1PatientRefund.Cols["Tray"].Width = Convert.ToInt32(_nWidth * 0.10);
        //        c1PatientRefund.Cols["sRefundTo"].Width = Convert.ToInt32(_nWidth * 0.14);
        //        c1PatientRefund.Cols["nRefundDate"].Width = Convert.ToInt32(_nWidth * 0.10);
        //        c1PatientRefund.Cols["Amount"].Width = Convert.ToInt32(_nWidth * 0.10);
        //        c1PatientRefund.Cols["sNoteDescription"].Width = Convert.ToInt32(_nWidth * 0.25);
        //        c1PatientRefund.Cols["sUserName"].Width = Convert.ToInt32(_nWidth * 0.11);
        //        c1PatientRefund.Cols["dtCreatedDateTime"].Width = Convert.ToInt32(_nWidth * 0.15);
        //        c1PatientRefund.Cols["dtCreatedDateTime"].Format = "MM/dd/yyyy hh:mm tt";
        //        c1PatientRefund.Cols["Status"].Width = Convert.ToInt32(_nWidth * 0.07);
        //        c1PatientRefund.Cols["Amount"].Format = "c";

        //        c1PatientRefund.Cols["nCloseDate"].DataType = typeof(System.DateTime);
        //        c1PatientRefund.Cols["nCloseDate"].Format = "MM/dd/yyyy";
        //        c1PatientRefund.Cols["nRefundDate"].DataType = typeof(System.DateTime);
        //        c1PatientRefund.Cols["nRefundDate"].Format = "MM/dd/yyyy";

        //        //c1PatientRefund.Cols["nClinicID"].Visible = false;
        //       // c1PatientRefund.Cols["nUserID"].Visible = false;
        //        c1PatientRefund.Cols["nEOBPaymentID"].Visible = false;
        //        c1PatientRefund.Cols["nPaymentTrayID"].Visible = false;
        //        c1PatientRefund.Cols["Remaining"].Visible = false;
        //        c1PatientRefund.Cols["nUserID"].Visible = false;
        //        c1PatientRefund.Cols["nPayerID"].Visible = false;
        //        c1PatientRefund.Cols["PaymentDate"].Visible = false;
        //        c1PatientRefund.Cols["CheckNumber"].Visible = false;
        //        c1PatientRefund.Cols["Company"].Visible = false;
        //        //c1PatientRefund.Cols["Claim"].Visible = false;

        //        if (c1PatientRefund.Rows.Count > 1)
        //        {
        //            c1RefundTotal.Cols[0].Width = 0;
        //            c1RefundTotal.Cols[1].Width = Convert.ToInt32(_nWidth * 0.10);
        //            c1RefundTotal.Cols[2].Width = Convert.ToInt32(_nWidth * 0.10);
        //            c1RefundTotal.Cols[3].Width = Convert.ToInt32(_nWidth * 0.14);
        //            c1RefundTotal.Cols[4].Width = Convert.ToInt32(_nWidth * 0.10);
        //            c1RefundTotal.Cols[5].Width = Convert.ToInt32(_nWidth * 0.10);
        //            c1RefundTotal.Cols[6].Width = Convert.ToInt32(_nWidth * 0.25);
        //            c1RefundTotal.Cols[7].Width = Convert.ToInt32(_nWidth * 0.11);
        //            c1RefundTotal.Cols[8].Width = Convert.ToInt32(_nWidth * 0.15);
        //            c1RefundTotal.Cols[9].Width = Convert.ToInt32(_nWidth * 0.07);
        //            c1RefundTotal.Cols[1].Caption = "Total :";
        //            c1RefundTotal.Cols[5].Caption = "$" + Convert.ToString(TotalRefund);
        //            setGridStyle(c1RefundTotal, 0, 1, 5);
        //        }
        //        else
        //        {
        //            c1RefundTotal.Cols[0].Width = 1300;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

        //    }

        //}


        //private long GetClaimTransactionID(Int64 _nClaimNo, string _subclaimNo, bool _isVoid)
        //{

        //    #region "To Fetch the TransactionID of Claim"

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
        //    object _TransactionId = null;
        //    string strQuery = "";
        //    DataTable _dtTransID = null;
        //    try
        //    {
        //        oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
        //        gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
        //        oDB.Connect(false);
        //        oDBPatameters.Add("@nClaimno", _nClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBPatameters.Add("@sSubClaimno", _subclaimNo, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBPatameters.Add("@bIsVoid", _isVoid, ParameterDirection.Input, SqlDbType.Bit);
        //        oDB.Retrive("BL_Get_TransactionID", oDBPatameters, out _dtTransID);
        //        if (_dtTransID != null && _dtTransID.Rows.Count > 0)
        //        {
        //            return Convert.ToInt64(_dtTransID.Rows[0]["nTransactionID"]);
        //        }
        //        oDB.Disconnect();
        //    }
        //    catch (Exception Ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        oDB.Dispose();
        //    }
        //    if (_TransactionId != null)
        //        return Convert.ToInt64(_TransactionId);
        //    else
        //        return 0;
        //    #endregion
        //    // throw new Exception("The method or operation is not implemented.");
        //}

        //private Boolean ChkClaimVoided(Int64 _nTransactionMstID)
        //{

        //    #region "To check Claim is voided or not"

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
        //    Boolean isVoided = false;
        //    DataTable _dtClaimVoided = null;
        //    try
        //    {
        //        oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
        //        gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
        //        oDB.Connect(false);
        //        oDB.Retrive_Query(" select bIsVoid from BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionMasterID=" + _nTransactionMstID, out _dtClaimVoided);
        //        if (_dtClaimVoided != null && _dtClaimVoided.Rows.Count > 0)
        //        {
        //            isVoided = Convert.ToBoolean(_dtClaimVoided.Rows[0]["bIsVoid"] == DBNull.Value ? false : true);
        //        }

        //        oDB.Disconnect();
        //        return isVoided;
        //    }
        //    catch (Exception Ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
        //        return isVoided;
        //    }
        //    finally
        //    {
        //        oDB.Dispose();

        //    }

        //    #endregion
        //    // throw new Exception("The method or operation is not implemented.");
        //}

        //private void OpenReserveForModify(int RowIndex)
        //{
        //    Int64 nEobPaymentId = 0;
        //    Int64 nPatientId = 0;
        //    Int64 nEobPayDetailId = 0;

        //    try
        //    {
        //        if (c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentID"].Index)).Trim() != "")
        //        { nEobPaymentId = Convert.ToInt64(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentID"].Index)); }

        //        if (c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentDetailID"].Index) != null && Convert.ToString(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentDetailID"].Index)).Trim() != "")
        //        { nEobPayDetailId = Convert.ToInt64(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentDetailID"].Index)); }

        //        if (c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nPatientID"].Index) != null && Convert.ToString(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nPatientID"].Index)).Trim() != "")
        //        { nPatientId = Convert.ToInt64(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nPatientID"].Index)); }

        //        frmPaymentReserveRemaningV2 ofrmPaymentReserveRemaning = new frmPaymentReserveRemaningV2( gloPMGlobal.DatabaseConnectionString, nPatientId, nEobPaymentId, nEobPayDetailId, true);
        //        ofrmPaymentReserveRemaning.PAccountID = _nPAccountId;
        //        ofrmPaymentReserveRemaning.ShowDialog();
        //        if (ofrmPaymentReserveRemaning.DialogResult == DialogResult.OK)
        //        {
        //            //FillReserves();
        //            tbPatientFinancial_SelectedIndexChanged(null, null);
        //        }
        //        ofrmPaymentReserveRemaning.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        ex = null;
        //    }
        //    finally
        //    { }
        //}

        //private void FillClaimOnHold()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    try
        //    {
        //        DataTable dtClaimOnHold = new DataTable();
        //        oParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nClinicID", gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

        //        oDB.Connect(false);
        //        oDB.Retrive("Patient_Financial_View_Header_ClaimOnHold", oParameters, out dtClaimOnHold);
        //        oDB.Disconnect();

        //        if (dtClaimOnHold.Rows.Count > 0)
        //        {
        //            string[] strArrClaims = dtClaimOnHold.Rows[0]["ClaimNo"].ToString().Split(',');
        //            for (int iClaimCount = 0; iClaimCount <= strArrClaims.Length - 1; iClaimCount++)
        //            {
        //                for (int iGridClaimCount = 1; iGridClaimCount <= c1FlexGridChargesClaims.Rows.Count - 1; iGridClaimCount++)
        //                {
        //                    if (c1FlexGridChargesClaims.GetData(iGridClaimCount, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString() == strArrClaims[iClaimCount].ToString())
        //                    {

        //                        CellStyle csSubCol;

        //                        csSubCol = c1FlexGridChargesClaims.Styles.Add("SubCol");
        //                        csSubCol.TextAlign = TextAlignEnum.LeftCenter;
        //                        csSubCol.BackColor = Color.FromArgb(255, 255, 255);
        //                        csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //                        csSubCol.TextEffect = TextEffectEnum.Flat;
        //                        csSubCol.ForeColor = Color.Red;
        //                        //csSubTotalRow.DataType = typeof(System.Decimal);
        //                        //CellRange subTotalRange;
        //                        //subTotalRange = c1FlexGridChargesClaims.GetCellRange(iRowNumber, 0, iRowNumber, iColCount);
        //                        //subTotalRange.Style = csSubCol;

        //                        c1FlexGridChargesClaims.Rows[iGridClaimCount].Style = csSubCol;

        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        ex = null;
        //    }
        //    finally
        //    {
        //        oDB.Dispose();
        //        oParameters.Dispose();
        //    }
        //}

        //private string getClinicName()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
        //    oDB.Connect(false);
        //    object _Result = oDB.ExecuteScalar_Query("SELECT COALESCE(sClinicName,'') AS sClinicName FROM Clinic_MST WITH (NOLOCK)");
        //    if (_Result.ToString() != "")
        //    { return _Result.ToString(); }
        //    else
        //    { return ""; }
        //}

        ////Bind the Patient Refund
        //private void FillPatientRefund()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
        //    string _sqlQuery = "";
        //    object _TotalRefund = null;
        //    decimal _totalrefund = 0;

        //    try
        //    {

        //         _sqlQuery = " SELECT  nRefundID,CloseDate,Tray, Company,  nPayerID,  sRefundTo,PaymentDate,CheckNumber, "
        //                    + " Amount, sNoteDescription,sUserName, nRefundDate, Status,nClinicID,nUserID, Remaining, nEOBPaymentID,"
        //                    + " nPaymentTrayID,dtCreatedDateTime FROM view_PatientPaymentReFunds_V2 where nClinicID = " + AppSettings.ClinicID;

        //         if (_nPatientID != 0)
        //         { _sqlQuery += " AND nPayerID = " + _nPatientID; }


        //         _sqlQuery = _sqlQuery + " order by CloseDate,nRefundDate desc";


        //        DataTable dtPatientRefund = new DataTable();
        //        oDB.Connect(false);
        //        oDB.Retrive_Query(_sqlQuery, out dtPatientRefund);
        //        oDB.Disconnect();

        //        if (dtPatientRefund.Rows.Count > 0)
        //        {
        //            tsb_ViewPatRefund.Enabled = true;
        //        }

        //        //Sum of the Refund amount.
        //        _TotalRefund = dtPatientRefund.Compute("SUM(Amount)", String.Empty);
        //        if (_TotalRefund != null && _TotalRefund.ToString() != "")
        //            _totalrefund = Convert.ToDecimal(_TotalRefund);

        //        c1PatientRefund.DataSource = dtPatientRefund.DefaultView;
        //        DesignRefundgrid(_totalrefund);

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //        if (oDB != null)
        //            oDB.Dispose();

        //    }
        //    finally
        //    {
        //        if (oDB != null)
        //            oDB.Dispose();
        //        if (_TotalRefund != null)
        //            _TotalRefund = null;
        //    }
        //}

        //private void FillAssociatedPatInsReserves()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
        //    DataTable _dtReserves = new DataTable();
        //    try
        //    {
        //        DesignReserveGrid();
        //        //_IsFormLoading = true;
        //        oDB.Connect(false);
        //        oDB.Retrive_Query("SELECT InsuarnceCompanyName,,nEOBPaymentID,nEOBID,nEOBDtlID,nEOBPaymentDetailID,nBillingTransactionID, " 
        //                        + " nBillingTransactionDetailID,nBillingTransactionLineNo,nDOSFrom,nDOSTo,nPatientID,nAmount,nPayMode, "
        //                        + " nRefEOBPaymentID,nRefEOBPaymentDetailID,nResEOBPaymentID,nResEOBPaymentDetailID,nAccountID, "
        //                        + " nAccountType,nMSTAccountID,nMSTAccountType,nPaymentMode,CheckNumber,nCheckAmount,nCheckDate, "
        //                        + " nPayerID,PatientName,sNoteDescription,sNoteCode,nPaymentNoteSubType,sUserName,UsedReserve, " 
        //                        + " AvailableReserve,nCloseDate,AssociationPatientID,AssociationPatient,AssociationMSTTransactionID, "
        //                        + " AssociationnTransactionID,AssociationClaim "
        //                        + " FROM view_PatientInsCompanyReserves_V2 where AssociationPatientID = " + _nPatientID, out _dtReserves);
        //        oDB.Disconnect();
        //        Decimal _dTotalToRes = 0;
        //        Decimal _dTotalAvlRes = 0;
        //        if (_dtReserves != null && _dtReserves.Rows.Count > 0)
        //        {
        //            int _rowIndex = 0;

        //            for (int i = 0; i < _dtReserves.Rows.Count; i++)
        //            {

        //                #region " Set Data "

        //                _rowIndex = c1Reserve.Rows.Add().Index;

        //                c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"]));
        //                c1Reserve.SetData(_rowIndex, COL_EOBID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBID"]));
        //                c1Reserve.SetData(_rowIndex, COL_EOBDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBDtlID"]));
        //                c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]));
        //                c1Reserve.SetData(_rowIndex, COL_BLTRANSACTIONID, Convert.ToInt64(_dtReserves.Rows[i]["nBillingTransactionID"]));
        //                c1Reserve.SetData(_rowIndex, COL_BLTRANDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nBillingTransactionDetailID"]));
        //                c1Reserve.SetData(_rowIndex, COL_BLTRANLINEID, Convert.ToString(_dtReserves.Rows[i]["nBillingTransactionLineNo"]));
        //                c1Reserve.SetData(_rowIndex, COL_DOSFROM, Convert.ToInt64(_dtReserves.Rows[i]["nDOSFrom"]));
        //                c1Reserve.SetData(_rowIndex, COL_DOSTO, Convert.ToString(_dtReserves.Rows[i]["nDOSTo"]));
        //                c1Reserve.SetData(_rowIndex, COL_PATIENTID, Convert.ToString(_dtReserves.Rows[i]["nPatientID"]));
        //                c1Reserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(_dtReserves.Rows[i]["PatientName"]));//Patient or Insurance Name
        //                c1Reserve.SetData(_rowIndex, COL_COMPANYNAME, Convert.ToString(_dtReserves.Rows[i]["InsuarnceCompanyName"]));
        //                string _originalPayment = "";
        //                _originalPayment = ((PaymentModeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])).ToString() + "# " + Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]) + " " + _dtReserves.Rows[i]["nCheckDate"] + " $ " + Convert.ToDecimal(_dtReserves.Rows[i]["nCheckAmount"]);
        //                c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

        //                c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]));
        //                //c1Reserve.SetData(_rowIndex, COL_TYPE, ((EOBPaymentSubType)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
        //                c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]));//Note
        //                c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(_dtReserves.Rows[i]["UsedReserve"]));//Used amount
        //                c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount
        //                c1Reserve.SetData(_rowIndex, COL_REFUND, 0);//Current amount to use from avaiable amount

        //                c1Reserve.SetData(_rowIndex, COL_PAYMODE, ((PaymentModeV2)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])));
        //                c1Reserve.SetData(_rowIndex, COL_REFEOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentID"]));
        //                c1Reserve.SetData(_rowIndex, COL_REFEOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentDetailID"]));
        //                c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"]));
        //                c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]));
        //                c1Reserve.SetData(_rowIndex, COL_ACCOUNTID, Convert.ToInt64(_dtReserves.Rows[i]["nAccountID"]));
        //                c1Reserve.SetData(_rowIndex, COL_ACCOUNTTYPE, Convert.ToInt32(_dtReserves.Rows[i]["nAccountType"]));
        //                c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTID, Convert.ToInt64(_dtReserves.Rows[i]["nMSTAccountID"]));
        //                c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTTYPE, Convert.ToInt32(_dtReserves.Rows[i]["nMSTAccountType"]));

        //                c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentID"]));
        //                c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentDetailID"]));
        //                c1Reserve.SetData(_rowIndex, COL_PAYMENTCLOSEDATE, _dtReserves.Rows[i]["nCloseDate"]);
        //                c1Reserve.SetData(_rowIndex, COL_PAYMENTMODE, Convert.ToInt64(_dtReserves.Rows[i]["nPayMode"]));
        //                c1Reserve.SetData(_rowIndex, COL_PAYMENTMODENO, Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]));
        //                c1Reserve.SetData(_rowIndex, COL_USERNAME, Convert.ToString(_dtReserves.Rows[i]["sUserName"]));
        //                c1Reserve.SetData(_rowIndex, COL_ASSO_CLAIMNO, Convert.ToString(_dtReserves.Rows[i]["AssociationClaim"]));

        //                c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTID, Convert.ToInt64(_dtReserves.Rows[i]["AssociationPatientID"]));
        //                c1Reserve.SetData(_rowIndex, COL_ASSO_PATIENTNAME, Convert.ToString(_dtReserves.Rows[i]["AssociationPatient"]));
        //                c1Reserve.SetData(_rowIndex, COL_ASSO_MSTTRANSACTIONID, Convert.ToInt64(_dtReserves.Rows[i]["AssociationMSTTransactionID"]));
        //                c1Reserve.SetData(_rowIndex, COL_ASSO_TRACKTRANSACTIONID, Convert.ToInt64(_dtReserves.Rows[i]["AssociationnTransactionID"]));


        //                _dTotalToRes = _dTotalToRes + Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]);
        //                _dTotalAvlRes = _dTotalAvlRes + Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]);

        //                #region " Set Styles "

        //                c1Reserve.SetCellStyle(_rowIndex, COL_REFUND, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

        //                #endregion " Set Styles "


        //                #endregion
        //            }
        //            DesignTotalReserveGrid();

        //            setGridStyle(c1InsReserveTotal, 0, COL_PAYMENTCLOSEDATE, c1InsReserveTotal.Cols.Count - 1);
        //            c1InsReserveTotal.SetData(0, COL_PAYMENTCLOSEDATE, "Total :");
        //            c1InsReserveTotal.SetData(0, COL_TORESERVES, "$" + _dTotalToRes);
        //            c1InsReserveTotal.SetData(0, COL_AVAILABLE, "$" + _dTotalAvlRes);
        //            c1InsReserveTotal.Width = c1Reserve.Width;
        //        }
        //        else
        //        {
        //            c1InsReserveTotal.Rows.Count = 0;
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    { dbEx.ERROR_Log(dbEx.ToString()); }
        //    catch (Exception ex)
        //    { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (_dtReserves != null) { _dtReserves.Dispose(); }
        //        //_IsFormLoading = false;
        //    }
        //}


        //private Boolean chkIsInsReserveRefundExist()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( gloPMGlobal.DatabaseConnectionString);
        //    DataTable _dtReserves = new DataTable();
        //    DataTable _dtRefundLog = new DataTable();
        //    Boolean _bIsDataExist = false;
        //    try
        //    {
        //        oDB.Connect(false);
        //        oDB.Retrive_Query(" SELECT  Reserves.nCreditID AS nEOBPaymentID "
        //                        + " FROM  BL_Reserve_Association WITH (NOLOCK) LEFT OUTER JOIN  Reserves "
        //                        + " ON   BL_Reserve_Association.nEOBPaymentID = Reserves.nCreditID "
        //                        + " WHERE (Reserves.nReserveType = 1) AND  BL_Reserve_Association.nPatientID =  " + _nPatientID, out _dtReserves);
        //        oDB.Disconnect();
        //        if (_dtReserves != null && _dtReserves.Rows.Count > 0)
        //        {
        //            _bIsDataExist = true;
        //        }
        //        else
        //        {
        //           _dtRefundLog = gloAccountsV2.gloPatientPaymentV2.GetPatientPaymentRefundLog(_nPatientID, gloPMGlobal.ClinicID);
        //            if (_dtRefundLog != null && _dtRefundLog.Rows.Count > 0)
        //            {
        //                _bIsDataExist = true;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        _bIsDataExist = false;

        //    }
        //    finally
        //    {
        //        _dtReserves.Dispose();
        //        _dtRefundLog.Dispose();
        //        if (oDB.Connect(false))
        //        { oDB.Disconnect(); }
        //        if (oDB != null)
        //        { oDB.Dispose(); }
        //    }
        //    return _bIsDataExist;
        //}

        #endregion

        #endregion


       
    }
} 
