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

namespace gloBilling
{
    public partial class frmPatientFinancialView : Form
    {

        #region "Variable declaration"

       //gloPatientStripControl.gloPatientStripControl oPatientControl = null;
        //added by mahesh s(apollo) For PAF 2011-06-28(yyyy-mm-dd)
        public gloStripControl.gloPatientStrip_FA oPatientControl = null;

        TabPage objtab = null;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        string _sqlDatabaseConnectionString = "";
        Int64 _nPatientID = 0;
        Int64 _nClinicID = 1;
        Int64 _nUserId = 0;
        Int64 _nTrasactionID;
        string _strUserName = "";
        string _strMessageBoxCaption = string.Empty;
        const int COL_NO = 11;
        const int COL_CPT_CODE = 2;
        const int COL_NOTE_DATA = 10;
        const int COL_NOTE_IMAGE = 11;
        const int COL_CLAIM_NO = 12;
        const int COL_TRANSACTION_MST_ID = 38;
        const int COL_DOS = 13;
        private int iSummarySelRow = 1;
        private int iChargesSelRow = 1;
        private int iPaymentSelRow = 1;
        private int iReserveSelRow = 1;

        private bool _blnIsClaimVoided;
        private Font _fBold = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
        private Font _fRegular = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        private Boolean _blnShowZeroBal = true;
        public Int64 _nEOBPaymentID = 0;
    //   string _strCloseDate = "";
        private string _strTagClaims = "Charges & Claims";
        private string _strTagPaymentReserves = "Patient Payments & Reserves";

      //  private bool _IsFormLoading = false;
        private bool _blnDisposed;
        private static frmPatientFinancialView frmPatFinView;


        string strReportServer = string.Empty;
        string strReportFolder = string.Empty;
        string strVirtualDir = string.Empty;


      
        string strParameter = string.Empty;
        string _UserName = string.Empty;
        string _conn = string.Empty;
        string _reportTitle = string.Empty;
        string reportParam = string.Empty;
    //    bool _IsgloStreamReport;
        System.Uri SSRSReportURL;
        private DataView _dv = new DataView();
        bool _IsPatientFinancialView = true;
        Char _AgingSortByInSummary = 'R';

        //Code added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
        Int64 _nPAccountId = 0;
        Int64 _nSelectedPatientId = 0;
        Int64 _nGuarantorId = 0;
        Int64 _nAccountPatientId = 0;

        // We need to use unmanaged code

        [DllImport("user32.dll")]

        // GetCursorPos() makes everything possible

        static extern bool GetCursorPos(ref Point lpPoint);
        
        // Nested Types
        private enum _tabType
        {
            Chronology = 6,
            Claim_Details = 1,
            Patient_Payment_and_Reserves = 2,
            Insurance_Remittance =7,
            Statement =4,
            Insurance_Reserves_and_Refunds = 5,
            Summary = 0,
            Report = 3,
            Party = 27
        }

        #region  " Grid Constants "

        const int COL_EOBPAYMENTID = 0;
        const int COL_EOBID = 1;
        const int COL_EOBDTLID = 2;
        const int COL_EOBPAYMENTDTLID = 3;
        const int COL_BLTRANSACTIONID = 4;
        const int COL_BLTRANDTLID = 5;
        const int COL_BLTRANLINEID = 6;
        const int COL_DOSFROM = 7;
        const int COL_DOSTO = 8;
       
        const int COL_PAYMENTCLOSEDATE = 9;
        const int COL_COMPANYNAME = 10;
        const int COL_ORIGINALPAYMENT = 11;//Check Number,Date,Amount
        const int COL_PATIENTID = 12;
       
        const int COL_ASSO_CLAIMNO = 13;

        const int COL_USERNAME = 14;
        const int COL_TORESERVES = 15;//Amount for reserve
        const int COL_TYPE = 16;//Copay,Advance,Other
        const int COL_NOTE = 17;//Note
        const int COL_AVAILABLE = 18;//Available amount
        const int COL_USERESERVE = 19;//Used Reserve
        const int COL_REFUND = 20;//Current amount to use from avaiable amount
        const int COL_PAYMODE = 21;
        const int COL_REFEOBPAYID = 22;
        const int COL_REFEOBPAYDTLID = 23;
        const int COL_ACCOUNTID = 24;
        const int COL_ACCOUNTTYPE = 25;
        const int COL_MSTACCOUNTID = 26;
        const int COL_MSTACCOUNTTYPE = 27;
        const int COL_RES_EOBPAYID = 28;
        const int COL_RES_EOBPAYDTLID = 29;
        const int COL_SOURCE = 30; //Patient or Insurance Name
        const int COL_PAYMENTMODE = 31;
        const int COL_PAYMENTMODENO = 32;

        const int COL_ASSO_PATIENTID = 33;
        const int COL_ASSO_PATIENTNAME = 34;
        const int COL_ASSO_TRACKTRANSACTIONID = 35;
        const int COL_ASSO_MSTTRANSACTIONID = 36;

        const int COL_COUNT = 37;


        const int COL_COUNT_REF = 19;

        const int COL_CLOSEDATE_REF = 0;
        const int COL_TRAY_REF = 1;
        const int COL_COMPANY_REF = 2;
        const int COL_CHECK_NO_REF = 3;
        const int COL_REFUND_DATE_REF = 4;
        const int COL_REFUND_AMOUNT_REF = 5;
        const int COL_USER_REF = 7;
        const int COL_CLAIMNO_REF = 6;
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
        const int COL_PatientName_REF = 18;

        #region  " Grid Constants "

    

        #endregion

        #endregion 

        #endregion

        #region "Constructor"

        //public frmPatientFinancialView()
        //{
        //    InitializeComponent();
        //}

        public frmPatientFinancialView(Int64 PatientID)
        {
            InitializeComponent();

            #region " Retrive ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _nClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _nClinicID = 1; }
            }
            else
            { _nClinicID = 1; }

            #endregion " Retrive MessageBOXCaption from AppSettings "

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _strMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _strMessageBoxCaption = "gloPM"; ;
                }
            }
            else
            { _strMessageBoxCaption = "gloPM"; ; }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _nUserId = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _nUserId = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _strUserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _strUserName = "";
            }

            #endregion

            _sqlDatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            _nPatientID = PatientID;

        }

        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this._blnDisposed))
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
                        System.Windows.Forms.ContextMenuStrip[] cntdtControls = { contextMenuChargeHistory };
                        System.Windows.Forms.Control[] cntControls = { contextMenuChargeHistory };
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
                        
                        
                        //if (contextMenuChargeHistory != null)
                        //{
                        //    gloGlobal.cEventHelper.RemoveAllEventHandlers(contextMenuChargeHistory);
                        //    if (contextMenuChargeHistory.Items != null)
                        //    {
                        //        contextMenuChargeHistory.Items.Clear();

                        //    }
                        //    contextMenuChargeHistory.Dispose();
                        //    contextMenuChargeHistory = null;
                        //}
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

        ~frmPatientFinancialView()
        {
            Dispose(false);
        }

        public static frmPatientFinancialView GetInstance(Int64 PatientID)
        {
            try
            {
                if (frmPatFinView == null)
                {
                    frmPatFinView = new frmPatientFinancialView(PatientID);
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
            gloC1FlexStyle.Style(c1FlexGridChargesClaims, false);
            gloC1FlexStyle.Style(c1FlexGrid_Statements, false);
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, this._nPatientID, this._nClinicID);
            try
            {

                objtab = tbPatientFinancial.TabPages["tbpgInsResAndRefund"];
                LoadPatientStrip(_nPatientID, 0, true);
                tbPatientFinancial_SelectedIndexChanged(sender, e);
                tbPatientFinancial.TabPages.RemoveAt(6);
                tbPatientFinancial.TabPages.RemoveAt(4);
                TabPage newPage = tbPatientFinancial.TabPages["tbpgInsResAndRefund"];
                if (! objPatFinacialView.chkIsInsReserveRefundExist())
                {
                    tbPatientFinancial.TabPages.RemoveAt(tbPatientFinancial.TabPages.IndexOf(newPage));
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
            this.PAHreportViewer.RefreshReport();
        }
        
        private void frmPatientFinancialView_FormClosed(object sender, FormClosedEventArgs e)
        {
            //added by mahesh s(Apollo) on 2011-06-28(yyyy-mm-dd) for null handling.
            if (oPatientControl != null)
                oPatientControl.Dispose();
            oPatientControl = null;
            //_fBold.Dispose();
            //_fBold = null;
            //_fRegular.Dispose();
            //_fRegular = null;

            this.Dispose();

        }

        void frmPatStmt_on_FromClose(object sender, EventArgs e)
        {
            if (!this._blnDisposed)
            {
                tbPatientFinancial_SelectedIndexChanged(null, null);
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
            tsb_ShowZeroBalance.Visible = false;
            tsb_HideZeroBalance.Visible = true;
            _blnShowZeroBal = false;
            tbPatientFinancial_SelectedIndexChanged(sender, e);
        }

        //private void ts_VoidPayment_Click(object sender, EventArgs e)
        //{
        //    DialogResult dlgRst = DialogResult.None;
        //    gloEOBPaymentPatient ogloPaymentPatient = new gloEOBPaymentPatient(_sqlDatabaseConnectionString);
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
        //                MessageBox.Show("Payment has been refunded so it may not be voided. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }

        //            blnIsVoid = Convert.ToBoolean(c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["bISVoid"].Index));
        //            if (blnIsVoid)
        //            {
        //                MessageBox.Show("Payment is already voided. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }
        //            //*****************End************************
        //            _strCloseDate = c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nCloseDate"].Index).ToString();
        //            dlgRst = MessageBox.Show("Do you want to void the payment? ", _strMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        //            try
        //            {
        //                if (dlgRst == DialogResult.Yes)
        //                {
        //                    frmVoidPayment ofrmVoid = new frmVoidPayment(_nEOBPaymentID);
        //                    ofrmVoid.ShowDialog(this);
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
        //            MessageBox.Show("No existing payment found for void. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception EX)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), true);
        //    }

        //}

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, _nSelectedPatientId,_nPAccountId, this._nClinicID);
            try
            {

                oPatientControl.FillDetails(_nPatientID, gloStripControl.FormName.Billing, 0, false);
              
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
                               //TabPage newPage = tbPatientFinancial.TabPages["tbpgInsResAndRefund"];
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
                    c1FlexGridChargesClaims.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, COL_CLAIM_NO);
                }
                tbPatientFinancial_SelectedIndexChanged(sender, e);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                if (objPatFinacialView != null)
                {
                    objPatFinacialView.Dispose();
                }
              
            }

        }

        private void tsb_HideZeroBalance_Click(object sender, EventArgs e)
        {
            tsb_ShowZeroBalance.Visible = true;
            tsb_HideZeroBalance.Visible = false;
            _blnShowZeroBal = true;
            tbPatientFinancial_SelectedIndexChanged(sender, e);
        }

        private void tsb_ShowDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1FlexGridAvailResrv != null && c1FlexGridAvailResrv.Rows.Count > 1)
                {
                    if (c1FlexGridAvailResrv.RowSel > 0)
                    {
                        OpenReserveForModify(c1FlexGridAvailResrv.RowSel);
                        oPatientControl.FillDetails(_nPatientID, gloStripControl.FormName.Billing, 0, false);
                    }
                }
                else
                {
                    MessageBox.Show("Reserve details not available. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            try
            {
                if (c1FlexGridChargesClaims.Rows.Count > 1)
                {
                    ModifyCharge();
                    tbPatientFinancial_SelectedIndexChanged(sender, e);
                    oPatientControl.FillDetails(_nPatientID, gloStripControl.FormName.Billing, 0, false);
                }
                else
                {
                    MessageBox.Show("Claim not available. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                nrefundid = c1PatientRefund.GetData(c1PatientRefund.RowSel, 0);


                frmPatientPayRefundView ofrmPatientPayRefundView = new frmPatientPayRefundView(_sqlDatabaseConnectionString, _nPatientID, Convert.ToInt64(nrefundid));
                ofrmPatientPayRefundView.ShowDialog(this);
                ofrmPatientPayRefundView.Dispose();
                tsb_Refresh.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void tsb_PatRefund_Click(object sender, EventArgs e)
        {
            try
            {
                if (_nPatientID > 0)
                {
                    if (c1FlexGridAvailResrv.Rows.Count > 1)
                    {
                        frmPatientRefund ofrmPatientRefundvoid = new frmPatientRefund(_sqlDatabaseConnectionString, _nPatientID);

                        //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd).
                        ofrmPatientRefundvoid.PAccountId = _nPAccountId;
                        ofrmPatientRefundvoid.SelectedPatientId = _nSelectedPatientId;
                        ofrmPatientRefundvoid.GuarantorId = _nGuarantorId;
                        ofrmPatientRefundvoid.AccountPatientId = _nAccountPatientId;

                        ofrmPatientRefundvoid.ShowDialog(this);
                        ofrmPatientRefundvoid.Dispose();
                        tsb_Refresh.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Cannot create a patient refund.  Patient refunds are made from available patient reserves. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select the patient.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsbViewHistory_Click(object sender, EventArgs e)
        {
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, this._nPatientID, this._nClinicID);
           
            if (_nPatientID > 0)
            {
                try
                {
                    Int64 ParamTransactionId = 0;
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
                                    Boolean chkVoid =objPatFinacialView.ChkClaimVoided(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionMSTID"].Index)));
                                    if (chkVoid == false)
                                        ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)), "", false);
                                    else
                                        ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)), "", true);

                                }
                            }
                            //frmClaimChargeHistory ofrmClaimChargeHistory = new frmClaimChargeHistory(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, ParamTransactionId);

                            // start abhisekh 07 sept 2010
                            //to get latest Transaction ID

                            gloBilling ogloBilling = new gloBilling(_sqlDatabaseConnectionString, "");
                            Int64 mainTransactionID = 0;
                            if (ParamTransactionId != 0)
                                mainTransactionID = ogloBilling.GetLastTransactionID(ParamTransactionId);

                            //frmClaimChargeHistory ofrmClaimChargeHistory = new frmClaimChargeHistory(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, ParamTransactionId);
                            frmClaimChargeHistory ofrmClaimChargeHistory = new frmClaimChargeHistory(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, mainTransactionID);

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
                    {
                    
                    }
            }
        }

        private void tsbPatientPmnt_Click(object sender, EventArgs e)
        {
            try
            {

                if (_nPatientID > 0)
                {
                    Int64 eobPaymentId = 0;
                    if (Convert.ToString(tbPatientFinancial.SelectedTab.Text).ToUpper() == _strTagPaymentReserves.ToUpper())
                    {
                        if (c1FlexGridPmnt.Rows.Count > 1)
                        {
                            if (c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nEOBPaymentID"].Index)) != "")
                            {
                                eobPaymentId = Convert.ToInt64(c1FlexGridPmnt.GetData(c1FlexGridPmnt.RowSel, c1FlexGridPmnt.Cols["nEOBPaymentID"].Index));
                                frmViewPatientPayment ofrmViewPatientPayment = new frmViewPatientPayment(_sqlDatabaseConnectionString, _nPatientID, _nClinicID, eobPaymentId);
                                ofrmViewPatientPayment.StartPosition = FormStartPosition.CenterScreen;
                                ofrmViewPatientPayment.ShowDialog(this);
                                ofrmViewPatientPayment.Dispose();
                                tbPatientFinancial_SelectedIndexChanged(sender, e);
                                oPatientControl.FillDetails(_nPatientID, gloStripControl.FormName.Billing, 0, false);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Payment details are not available. ", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Please select the patient.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                                //ModifyPatientTemplate();
                                ViewPatientStatementTemplate();
                                // tsb_Refresh_Click(sender, e);

                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Statement details are not available.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Added By Shweta -20100812
        private void tsbPatientStmtNotes_Click(object sender, EventArgs e)
        {
            frmPatientStatementNotes frmPSN = new frmPatientStatementNotes(_sqlDatabaseConnectionString, _nPatientID);
            try
            {
                frmPSN.ShowDialog(this);
                frmPSN.Dispose();
                tsb_Refresh_Click(null,null);        
                frmPSN = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        //Added By Shweta 20100812
        private void tsb_btnNewStatement_Click(object sender, EventArgs e)
        {
            Statement.frmRpt_Revised_PatientStatement frmPatStmt = new global::gloBilling.Statement.frmRpt_Revised_PatientStatement(_sqlDatabaseConnectionString, _nPatientID, _IsPatientFinancialView);
            frmPatStmt.on_FromClose += new global::gloBilling.Statement.frmRpt_Revised_PatientStatement.onFromClose(frmPatStmt_on_FromClose);
            frmPatStmt.MdiParent = this.ParentForm;       
            frmPatStmt.Show();  
            frmPatStmt.generateIndividualStmt(); 
            frmPatStmt.WindowState = FormWindowState.Maximized;
        }

        private void tbVoidStatment_Click(object sender, EventArgs e)
        {
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, _nSelectedPatientId,_nPAccountId, this._nClinicID);
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
                        DialogResult dlgRst = MessageBox.Show("Do you want to void the statement? ", _strMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dlgRst == DialogResult.Yes)
                        {
                            frmVoidStatmentBatch Objfrm = new frmVoidStatmentBatch(MasterId, DetailId, false);
                            Objfrm.ShowDialog(this);
                            Objfrm.Dispose();
                            Objfrm = null;
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
                        }
                    }
                    else
                    {
                        MessageBox.Show("Statement is already voided.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Statement details are not available.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
     
        #endregion

        #region 'Radio button Event'

        private void rbtn_Claim_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtn_Claim.Checked)
            {
                this.rbtn_Claim.Font = this._fBold;
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
                this.rbtn_DOS.Font = this._fBold;
            }
            else
            {
                this.rbtn_DOS.Font = this._fRegular;
            }
            tbPatientFinancial_SelectedIndexChanged(sender, e);

        }

        private void rbtn_Claim_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.rbtn_Claim.Checked)
            {
                this.rbtn_Claim.Font = this._fBold;
            }
            else
            {
                this.rbtn_Claim.Font = this._fRegular;
            }
            tbPatientFinancial_SelectedIndexChanged(sender, e);
        }

        private void rbtn_Responsibility_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtn_Responsibility.Checked)
            {
                this.rbtn_Responsibility.Font = this._fBold;
            }
            else
            {
                this.rbtn_Responsibility.Font = this._fRegular;
            }
            tbPatientFinancial_SelectedIndexChanged(sender, e);


        }

        private void rbtn_DOS_Summary_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtn_DOS_Summary.Checked)
            {
                this.rbtn_DOS_Summary.Font = this._fBold;
            }
            else
            {
                this.rbtn_DOS_Summary.Font = this._fRegular;
            }
            tbPatientFinancial_SelectedIndexChanged(sender, e);
        }
                
        #endregion

        #region "Commented Code By Shweta - 20112306 "
        //private void tbPatientFinancial_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DBLayer oDB = new DBLayer(this._sqlDatabaseConnectionString);
        //    DBParameters oParameters = new DBParameters();
        //    DataSet dsPatFinView = new DataSet();
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
        //                    dsPatFinView = this.fillgridData("Patient_Financial_View_Reserve", oParameters, oDB);
        //                    dsPayment = this.fillgridData("Patient_Financial_View_PatientPayment", oParameters, oDB);
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
        //                    oParameters.Add("@nPatientID", this._nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //                    oParameters.Add("@nClinicID", this._nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //                    oParameters.Add("@ZeroBalFlag", sZeroFlag, ParameterDirection.Input, SqlDbType.Bit);
        //                    oParameters.Add("@SortByFlag", sSort, ParameterDirection.Input, SqlDbType.Int);
        //                    oDB.Connect(false);
        //                    oDB.Retrive("Patient_Financial_View_Claims_Charges", oParameters, out dsPatFinView);
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
        //                    dsPatFinView = this.fillgridData("SP_GET_Chronology", oParameters, oDB);
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
        //                FillReserves();
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
        //                    oParameters.Add("@nPatientID", this._nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //                    oParameters.Add("@nClinicID", this._nClinicID, ParameterDirection.Input, SqlDbType.BigInt);   
        //                    oParameters.Add("@nSortBy", this._AgingSortByInSummary, ParameterDirection.Input, SqlDbType.Char);                        
        //                    oDB.Connect(false);
        //                    oDB.Retrive("Patient_Financial_View_Summary", oParameters, out dsPatFinView);
        //                    oDB.Disconnect();                            
        //                    dsPatFinView.Tables[0].TableName = "Summary";
        //                    dsPatFinView.Tables["Summary"].Columns.Add("Total", typeof(System.Decimal));
        //                    decimal decZeroThirty = 0;
        //                    decimal decThirtySixty = 0;
        //                    decimal decSixtyNinety = 0;
        //                    decimal decNinetyHundredTwenty = 0;
        //                    decimal decHundredTwentyPlus = 0;
        //                    decimal decTotal = 0;

        //                    if (dsPatFinView.Tables["Summary"].Rows.Count > 0)
        //                    {

        //                        for (cntr = 0; cntr <= (dsPatFinView.Tables["Summary"].Rows.Count - 1); cntr++)
        //                        {
        //                            dsPatFinView.Tables["Summary"].Rows[cntr]["Total"] = Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["0-30"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["0-30"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["31-60"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["31-60"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["61-90"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["61-90"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["91-120"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["91-120"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["120+"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["120+"]);
        //                            decZeroThirty += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["0-30"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["0-30"]);
        //                            decThirtySixty += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["31-60"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["31-60"]);
        //                            decSixtyNinety += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["61-90"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["61-90"]);
        //                            decNinetyHundredTwenty += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["91-120"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["91-120"]);
        //                            decHundredTwentyPlus += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["120+"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntr]["120+"]);
        //                            decTotal += Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntr]["Total"]);
        //                        }
        //                        dsPatFinView.Tables["Summary"].Rows.Add();
        //                        dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["ResponsibleParty"] = "Total :";
        //                        dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["0-30"] = decZeroThirty;
        //                        dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["31-60"] = decThirtySixty;
        //                        dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["61-90"] = decSixtyNinety;
        //                        dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["91-120"] = decNinetyHundredTwenty;
        //                        dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["120+"] = decHundredTwentyPlus;
        //                        dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["Total"] = decTotal;
        //                        dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["dtLastBilled"] = DBNull.Value;
        //                        dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]["dtLastRemitteded"] = DBNull.Value;

        //                    }
        //                    //if (dsPatFinView.Tables["Summary"].Rows.Count > 0)
        //                    //{
        //                    //    for (int cntrRow = 0; cntrRow <= (dsPatFinView.Tables["Summary"].Rows.Count - 1); cntrRow++)
        //                    //    {
        //                    //        dsPatFinView.Tables["Summary"].Rows[cntrRow]["Total"] = Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntrRow]["0-30"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntrRow]["0-30"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntrRow]["31-60"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntrRow]["31-60"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntrRow]["61-90"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntrRow]["61-90"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntrRow]["91-120"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntrRow]["91-120"]) + Convert.ToDecimal(dsPatFinView.Tables["Summary"].Rows[cntrRow]["120+"] == DBNull.Value ? 0 : dsPatFinView.Tables["Summary"].Rows[cntrRow]["120+"]);
        //                    //    }
        //                    //}

        //                    this.c1FlexGridSummary.DataMember = "Summary";
        //                    this.c1FlexGridSummary.DataSource = dsPatFinView;
        //                    if (c1FlexGridSummary.Rows.Count > 1)
        //                    {
        //                        setGridStyle(c1FlexGridSummary, c1FlexGridSummary.Rows.Count - 1, 0, 8);
        //                        //c1FlexGridSummary.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
        //                        //c1FlexGridSummary.Refresh();

        //                        c1FlexGridSummary.Row = iSummarySelRow;


        //                    }
        //                    //if (dsPatFinView.Tables["Summary"].Rows.Count > 0)
        //                    //{
        //                    //    dtTotalReserves = new DataTable();
        //                    //    dtTotalReserves = dsPatFinView.Tables["Summary"].Clone();
        //                    //    dtTotalReserves.TableName = "Total_Summary";
        //                    //    dtTotalReserves.ImportRow(dsPatFinView.Tables["Summary"].Rows[dsPatFinView.Tables["Summary"].Rows.Count - 1]);
        //                    //    dsPatFinView.Tables.Add(dtTotalReserves);
        //                    //    dsPatFinView.Tables["Total_Summary"].Rows[0]["ResponsibleParty"] = "";
        //                    //    dsPatFinView.Tables["Total_Summary"].Rows[0]["0-30"] = decZeroThirty;
        //                    //    dsPatFinView.Tables["Total_Summary"].Rows[0]["31-60"] = decThirtySixty;
        //                    //    dsPatFinView.Tables["Total_Summary"].Rows[0]["61-90"] = decSixtyNinety;
        //                    //    dsPatFinView.Tables["Total_Summary"].Rows[0]["91-120"] = decNinetyHundredTwenty;
        //                    //    dsPatFinView.Tables["Total_Summary"].Rows[0]["120+"] = decHundredTwentyPlus;
        //                    //    dsPatFinView.Tables["Total_Summary"].Rows[0]["Total"] = decTotal;
        //                    //    dsPatFinView.Tables["Total_Summary"].Rows[0]["dtLastBilled"] = DBNull.Value;
        //                    //    dsPatFinView.Tables["Total_Summary"].Rows[0]["dtLastRemitteded"] = DBNull.Value;

        //                    //    this.c1FlexGridSummaryTotal.Rows[0].Visible = false;
        //                    //    this.c1FlexGridSummaryTotal.DataMember = "Total_Summary";
        //                    //    this.c1FlexGridSummaryTotal.DataSource = dsPatFinView;
        //                    //}
        //                    //else
        //                    //{
        //                    //    this.c1FlexGridSummaryTotal.Rows[0].Visible = false;
        //                    //    this.c1FlexGridSummaryTotal.DataMember = "Summary";
        //                    //    this.c1FlexGridSummaryTotal.DataSource = dsPatFinView;
        //                    //}

                            

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
        //                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_sqlDatabaseConnectionString);
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

        //                string reportParam = "&suser=" + _strUserName + "&Practice=" + getClinicName();// +"&Conn=" + _sqlDatabaseConnectionString;
        //                //this.Text = _reportTitle;

        //                List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();





        //                 SSRSReportURL = new Uri("http://" + strReportServer + "/" + strVirtualDir);

        //                PAHreportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
        //                PAHreportViewer.ServerReport.ReportServerUrl = SSRSReportURL;


                      
        //                    //axWebBrowser1.Navigate("http://" + strReportServer + "/" + strVirtualDir + "?/" + strReportFolder + "/" + _reportName + reportParam + "&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=true");
        //                PAHreportViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + "rptPatientAccountHistory";// +reportParam;
        //                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _strUserName, false));
        //                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
        //                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("nPatientID", _nPatientID.ToString() , false));
        //                //paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Conn", _sqlDatabaseConnectionString, false));
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

        #region "New Code Added By Shweta - 20112306"

        private void tbPatientFinancial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Patient_Payment_and_Reserves))
                {
                    tsbViewHistory.Visible = false;
                    tsb_ShowDetails.Visible = true;
                    tsbPatientPmnt.Visible = true;
                    tsbPatientPmnt.Enabled = true;
                    tsb_Modify.Visible = false;
                    tsb_ShowZeroBalance.Visible = false;
                    tsb_HideZeroBalance.Visible = false;
                    tsb_PatRefund.Visible = true;
                    tsb_ViewPatRefund.Visible = true;
                    tsb_PatRefund.Enabled = true;
                    tsb_ViewPatRefund.Enabled = false;
                    tsb_ViewStmt.Visible = false;
                    tsbPatientStmtNotes.Visible = false;
                    tsb_btnNewStatement.Visible = false;
                    tbVoidStatment.Visible = false;
                    FillPatientPayment_Reserves_Refunds();
                }
                else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Claim_Details))
                {

                    tsb_ShowDetails.Enabled = true;
                    tsbPatientPmnt.Visible = false;
                    tsbPatientPmnt.Enabled = true;
                    tsb_ShowDetails.Visible = false;
                    tsb_PatRefund.Visible = false;
                    tsb_ViewPatRefund.Visible = false;
                    tsb_ViewStmt.Visible = false;
                    tsbPatientStmtNotes.Visible = false;
                    tsb_btnNewStatement.Visible = false;
                    tbVoidStatment.Visible = false;
                    if (_blnShowZeroBal == true)
                    {
                        tsb_HideZeroBalance.Visible = false;
                        tsb_ShowZeroBalance.Visible = true;
                    }
                    else
                    {
                        tsb_HideZeroBalance.Visible = true;
                        tsb_ShowZeroBalance.Visible = false;
                    }

                    tsb_Modify.Visible = true;
                    short sZeroFlag = 0;
                    int sSort = 1;

                    if (tsb_ShowZeroBalance.Visible == true)
                        sZeroFlag = 0;
                    else
                        sZeroFlag = 1;

                    if (this.rbtn_Claim.Checked)
                    {
                        sSort = 1;
                    }
                    else if (this.rbtn_DOS.Checked)
                    {
                        sSort = 2;
                    }
                    FillClaim_Charges(sZeroFlag, sSort);
                }
                else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Chronology))
                {
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
                    tbVoidStatment.Visible = false;
                    FillChronology();
                }
                else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Insurance_Remittance))
                {
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
                    tbVoidStatment.Visible = false;
                }
                else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Statement))
                {
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
                    tbVoidStatment.Visible = true;
                    FillStatement();
                }
                else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Insurance_Reserves_and_Refunds))
                {
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
                    tbVoidStatment.Visible = false;
                    FillInsuranceReserves();
                    LoadInsuranceRefundLog();
                }
                else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Summary))
                {
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
                    tbVoidStatment.Visible = false;

                    if (this.rbtn_Responsibility.Checked)
                    {
                        _AgingSortByInSummary = 'R';
                    }
                    else if (this.rbtn_DOS_Summary.Checked)
                    {
                        _AgingSortByInSummary = 'S';
                    }
                    FillSummary();
                }
                else if (this.tbPatientFinancial.SelectedIndex == Convert.ToInt32(_tabType.Report))
                {
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
                    tbVoidStatment.Visible = false;
                    GenerateReport();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }       

        #endregion

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

            iChargesSelRow = c1FlexGridChargesClaims.RowSel;

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
                if (c1FlexGridTotalChargesClaims.Cols[e.Col].Name.ToString().ToUpper() == "SplitClaimNumber".ToUpper())
                    c1FlexGridTotalChargesClaims.Cols[e.Col].Width = c1FlexGridChargesClaims.Cols[e.Col].Width + 19;
                else if (c1FlexGridTotalChargesClaims.Cols[e.Col].Name.ToString() == "")
                    c1FlexGridTotalChargesClaims.Cols[e.Col].Width = c1FlexGridChargesClaims.Cols[e.Col].Width - 19;
                else
                    c1FlexGridTotalChargesClaims.Cols[e.Col].Width = c1FlexGridChargesClaims.Cols[e.Col].Width;
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
                c1FlexGridPmntTotal.Cols[e.Col].Width = c1FlexGridPmnt.Cols[e.Col].Width;

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
                c1FlexGridTotalAvailResrv.Cols[e.Col].Width = c1FlexGridAvailResrv.Cols[e.Col].Width;

            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
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
            //                    ofrmNotes = new frmNotes(this._sqlDatabaseConnectionString, this._nClinicID, Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["nTransactionID"]), Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["ntransactiondetailid"]), Convert.ToInt64(this.c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionLineNo"]), oNotes);
            //                    ofrmNotes.nTransactionMasterDetailID = Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionDetailMSTID"]);
            //                    ofrmNotes.nChildTransactionID = Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionMSTID"]);
            //                    ofrmNotes.FormName = "PatientFinancialView";
            //                    ofrmNotes._IsVoidNote = true;
            //                    ofrmNotes.IsVoidShowNote = true;
            //                    ofrmNotes.nChildTransactionID = Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["nTransactionID"]);
            //                }
            //                else
            //                {
            //                    ofrmNotes = new frmNotes(this._sqlDatabaseConnectionString, this._nClinicID, Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["nTransactionID"]), Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["ntransactiondetailid"]), Convert.ToInt64(this.c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionLineNo"]), oNotes);
            //                    ofrmNotes.nTransactionMasterDetailID = Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionDetailMSTID"]);
            //                    ofrmNotes.nChildTransactionID = Convert.ToInt64(c1FlexGridChargesClaims.Rows[hitInfo.Row]["nTransactionMSTID"]);
            //                    ofrmNotes.FormName = "PatientFinancialView";
            //                    ofrmNotes._IsVoidNote = false;
            //                    ofrmNotes.IsVoidShowNote = false;
            //                    ofrmNotes.nChildTransactionID = Convert.ToInt64(dsPatFinView.Tables["Transaction"].Rows[0]["nTransactionID"]);
            //                }
            //                // *****************End********************************
            //                ofrmNotes.ShowDialog(this);
            //                if (ofrmNotes.IsUpdated)
            //                {
            //                    DBLayer oDB = new DBLayer(this._sqlDatabaseConnectionString);
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

            try
            {
                HitTestInfo hitInfo = this.c1FlexGridChargesClaims.HitTest(e.X, e.Y);
                if (c1FlexGridChargesClaims.Rows.Count > 1)
                {
                    if (hitInfo.Row != 0)
                    {
                        ModifyCharge();
                        tbPatientFinancial_SelectedIndexChanged(sender, e);
                        oPatientControl.FillDetails(_nPatientID, gloStripControl.FormName.Billing, 0, false);
                    }
                }
                else
                {
                    MessageBox.Show("Claim not available.", _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    oPatientControl.FillDetails(_nPatientID, gloStripControl.FormName.Billing, 0, false);

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
                tsb_ShowDetails.Enabled = false;
            }
            else if (((C1.Win.C1FlexGrid.C1FlexGrid)sender).Name == "c1FlexGridAvailResrv")
            {
                tsb_ShowDetails.Enabled = true;
                //ts_VoidPayment.Enabled = false;
                tsbPatientPmnt.Enabled = false;
            }
            else
            {
                //ts_VoidPayment.Enabled = false;
                tsbPatientPmnt.Enabled = false;
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1RefundTotal_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            try
            {
                c1PatientRefund.Cols[e.Col].Width = c1RefundTotal.Cols[e.Col].Width;

            }
            //*************** End ********
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1FlexGridChargesClaims_MouseMove(object sender, MouseEventArgs e)
        {
            if (c1FlexGridChargesClaims.HitTest(e.X, e.Y).Column >= 14 && c1FlexGridChargesClaims.HitTest(e.X, e.Y).Column <= 27)
            {
                gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location, true);
            }
            else
            {
                gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
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
                    nrefundid = c1PatientRefund.GetData(c1PatientRefund.RowSel, 0);
                    frmPatientPayRefundView ofrmPatientPayRefundView = new frmPatientPayRefundView(_sqlDatabaseConnectionString, _nPatientID, Convert.ToInt64(nrefundid));
                    ofrmPatientPayRefundView.ShowDialog(this);
                    ofrmPatientPayRefundView.Dispose();
                    //frmPatientPayRefundVoid ofrmPatientRefund = new frmPatientPayRefundVoid(_sqlDatabaseConnectionString, _nPatientID);
                    //ofrmPatientRefund.ShowDialog(this);
                    //ofrmPatientRefund.Dispose();
                    tsb_Refresh.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void c1PatientRefund_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1FlexGridAvailResrv_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1FlexGridPmnt_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
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
            int _rowIndex = 0;
            int _xCord = 0;
            int _yCord = 0;
            int _Height = 0;
            _Height = c1FlexGrid_Statements.Rows[0].Height + 10;
            DataTable dt = new DataTable();
            Point defPnt = new Point();
            GetCursorPos(ref defPnt);
            _xCord = defPnt.X;
            _yCord = defPnt.Y;
           // SqlConnection _con = new SqlConnection(this._sqlDatabaseConnectionString);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
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
                //_con.Close();

                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oParameters.Add("@nPAccountID", _nPAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
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
                if (oDB != null) { oDB.Dispose(); }
            }

            if (dsPatFinView != null)
                dt = dsPatFinView.Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                _rowIndex = c1FlexGrid_Statements.RowSel - 1;
                _voidDateTime = Convert.ToDateTime(dt.Rows[_rowIndex]["VoidDateTime"]);
                _voidUserName = Convert.ToString(dt.Rows[_rowIndex]["UserName"]);
                _voidNotes = Convert.ToString(dt.Rows[_rowIndex]["Notes"]);
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
                c1InsRefundTotal.Cols[e.Col].Width = c1InsuranceRefundLog.Cols[e.Col].Width;

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
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1InsuranceRefundLog_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1InsuranceRefundLog_Click(object sender, EventArgs e)
        {

        }


        #endregion

        #region "PatientStrip Events"

        private void oPatientControl_PatientChanged(object sender, EventArgs e)
        {
            try
            {
                //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
                _nPatientID = oPatientControl.PatientID;
                _nSelectedPatientId = oPatientControl.CmbSelectedPatientID;
                _nPAccountId = oPatientControl.PAccountID; ;
                _nAccountPatientId = oPatientControl.AccountPatientID;
                _nGuarantorId = oPatientControl.GuarantorID;
                tsb_Refresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void oPatientControl_OnAccountChanged(object sender, EventArgs e)
        {
            try
            {

                //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
                _nPatientID = oPatientControl.PatientID;
                _nSelectedPatientId = oPatientControl.CmbSelectedPatientID;
                _nPAccountId = oPatientControl.PAccountID; ;
                _nAccountPatientId = oPatientControl.AccountPatientID;
                _nGuarantorId = oPatientControl.GuarantorID;
                tsb_Refresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        //private void oPatientControl_OnAccountPatientChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
        //        _nPatientID = oPatientControl.PatientID;
        //        _nSelectedPatientId = oPatientControl.CmbSelectedPatientID;
        //        _nPAccountId = oPatientControl.PAccountID; ;
        //        _nAccountPatientId = oPatientControl.AccountPatientID;
        //        _nGuarantorId = oPatientControl.GuarantorID;
        //        tsb_Refresh_Click(sender, e);
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
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
            _nPatientID = oPatientControl.PatientID;
            tsb_Refresh_Click(sender, e);

            //}
        }

        #endregion "PatientStrip Events"
             
        #endregion

        #region "Function"

        public bool IsClaimVoided
        {
            get
            {
                return this._blnIsClaimVoided;
            }
            set
            {
                this._blnIsClaimVoided = value;
            }
        }

        private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        {
            try
            {

                if (oPatientControl != null)
                {
                    for (int iCntrlCount = 0; iCntrlCount < this.Controls.Count; iCntrlCount++)
                    {
                        if (oPatientControl.Name == this.Controls[iCntrlCount].Name)
                        {
                            this.Controls.RemoveAt(iCntrlCount);
                            break;
                        }
                    }
                    try
                    {
                        oPatientControl.OnPatientChanged -= new gloStripControl.gloPatientStrip_FA.PatientChanged(oPatientControl_PatientChanged);
                        oPatientControl.OnAccountChanged -= new gloStripControl.gloPatientStrip_FA.AccountChangedHandler(oPatientControl_OnAccountChanged);
 
                    }
                    catch { }
                    oPatientControl.Dispose();
                    oPatientControl = null;
                }
                //changed by mahesh s on 2011-06-28(yyyy-mm-dd)
                //oPatientControl = new gloPatientStripControl.gloPatientStripControl(_sqlDatabaseConnectionString, SearchEnable);
                oPatientControl = new gloStripControl.gloPatientStrip_FA(gloStripControl.FormName.PatientAccountView);

                //oPatientControl.ControlSize_Changed += new gloPatientStripControl.gloPatientStripControl.ControlSizeChanged(oPatientControl_ControlSize_Changed);
                //oPatientControl.OnPatientSearchKeyPress += new gloStripControl.gloPatientStrip_FA.PatientSearchKeyPressHandler(oPatientControl_OnPatientSearchKeyPress);
                //oPatientControl.PatientModified += new gloPatientStripControl.gloPatientStripControl.Patient_Modified(oPatientControl_PatientModified);
                //oPatientControl.btnDownEnable = true;
                //oPatientControl.btnUpEnable = true;
                //oPatientControl.DTP.Visible = false;
                //oPatientControl.SearchOnPatientCode = true;
                //oPatientControl.ShowNotesAlerts = true;
                //oPatientControl.ShowTotalBalance = true;
                //oPatientControl.HideGuarantor = false;
                //oPatientControl.ShowUpDown();  
                oPatientControl.OnPatientChanged += new gloStripControl.gloPatientStrip_FA.PatientChanged(oPatientControl_PatientChanged);
                oPatientControl.OnAccountChanged += new gloStripControl.gloPatientStrip_FA.AccountChangedHandler(oPatientControl_OnAccountChanged);
                //oPatientControl.OnAccountPatientChanged += new gloStripControl.gloPatientStrip_FA.AccountPatientChangedHandler(oPatientControl_OnAccountPatientChanged);
                oPatientControl.FillDetails(PatientId, gloStripControl.FormName.Billing, PatientProviderId, false);

                this.Controls.Add(oPatientControl);
                oPatientControl.Dock = DockStyle.Top;
                oPatientControl.SendToBack();
                oPatientControl.Padding = new Padding(3, 0, 3, 0);
                pnlToolStrip.SendToBack();

                //Code added by Sai Krishna date on 2011-06-28(yyyy-mm-dd)
                _nPatientID = oPatientControl.PatientID;
                _nSelectedPatientId = oPatientControl.CmbSelectedPatientID;
                _nPAccountId = oPatientControl.PAccountID; ;
                _nAccountPatientId = oPatientControl.AccountPatientID;
                _nGuarantorId = oPatientControl.GuarantorID;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
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
                oParameters.Add("@nPatientID", this._nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", this._nClinicID, ParameterDirection.Input, SqlDbType.BigInt);
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
         //   csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
            try
            {
                if (C1Flex.Styles.Contains("SubTotalRow"))
                {
                    csSubTotalRow = C1Flex.Styles["SubTotalRow"];
                }
                else
                {
                    csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
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
                csSubTotalRow.Format = "c";
                csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);  //FromArgb(168,192,242);
                csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                csSubTotalRow.ForeColor = Color.Blue;
                csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;
            }
    //        csSubCol = C1Flex.Styles.Add("SubCol");
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
     
            //csSubTotalRow.DataType = typeof(System.Decimal);
         
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
            EOBPaymentSubType BillingNoteType = EOBPaymentSubType.None;
            NoteType blNoteType = NoteType.GeneralNote;
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, this._nPatientID, this._nClinicID);
            DBLayer oDB = new DBLayer(this._sqlDatabaseConnectionString);
            DBParameters oDBParameters = new DBParameters();          
            DataSet dsNotes = new DataSet();
            objPatFinacialView.GetChargeNotes(oDBParameters,oDB,nTransactionID,nTransactionDetailID, out dsNotes);
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
                                    BillingNoteType = EOBPaymentSubType.Charges_BillingNote;
                                    break;

                                case 18:
                                    BillingNoteType = EOBPaymentSubType.Charges_StatementNote;
                                    break;

                                case 19:
                                    BillingNoteType = EOBPaymentSubType.Charges_InternalNote;
                                    break;

                                case 0:
                                    BillingNoteType = EOBPaymentSubType.None;
                                    break;
                            }
                            int nNote = Convert.ToInt32(dsNotes.Tables[0].Rows[iNotesCnt]["nNoteType"].ToString());
                            if (nNote == 0)
                            {
                                blNoteType = NoteType.GeneralNote;
                            }
                            if (nNote == 10)
                            {
                                blNoteType = NoteType.Void_Note;
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
            DBLayer oDB = new DBLayer(this._sqlDatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            oParameters.Add("@nMasterTransactionID", nMstTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
            oParameters.Add("@nMasterTransactionDetailID", nMstTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);

            oDB.Connect(false);
            oDB.Retrive("BL_SELECT_TransactionDetail", oParameters, out dsTransaction);
            oDB.Disconnect();
            return dsTransaction;
        }

        private void ModifyCharge()
        {
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, this._nPatientID, this._nClinicID);
            try
            {
                Int64 ParamTransactionId = 0;
                //Int64 _ParamClaimNo = 0;
                gloBilling ogloBilling = new gloBilling(_sqlDatabaseConnectionString, "");

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
                                ogloBilling.ShowModifyCharges(_nPatientID, ParamTransactionId, true);
                            }
                            else
                            {
                                ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(0, c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-"))), c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-") + 1), false);
                                //_ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "", false);
                                if ( ParamTransactionId != 0)
                                    ogloBilling.ShowModifyCharges(_nPatientID, ParamTransactionId, this);
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
                                ogloBilling.ShowModifyCharges(_nPatientID, ParamTransactionId, true);
                            }
                            else
                            {
                                ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)), "", false);
                                //_ModifyClaimTransactionID = GetClaimTransactionID(_ParamClaimNo, "", false);
                                if (ParamTransactionId != 0)
                                    ogloBilling.ShowModifyCharges(_nPatientID, ParamTransactionId, this);
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
            finally
            {

                if (objPatFinacialView != null)
                {
                    objPatFinacialView.Dispose();                
                }
            }
        }
      
        //Changed By Shweta 20112406
        private void FillInsuranceReserves()
        {
            DataTable _dtReserves = new DataTable();
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, _nPatientID, _nClinicID);
            try
            {
                DesignReserveGrid();
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
                        c1Reserve.SetData(_rowIndex, COL_EOBID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBID"]));
                        c1Reserve.SetData(_rowIndex, COL_EOBDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBDtlID"]));
                        c1Reserve.SetData(_rowIndex, COL_EOBPAYMENTDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]));
                        c1Reserve.SetData(_rowIndex, COL_BLTRANSACTIONID, Convert.ToInt64(_dtReserves.Rows[i]["nBillingTransactionID"]));
                        c1Reserve.SetData(_rowIndex, COL_BLTRANDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nBillingTransactionDetailID"]));
                        c1Reserve.SetData(_rowIndex, COL_BLTRANLINEID, Convert.ToString(_dtReserves.Rows[i]["nBillingTransactionLineNo"]));
                        c1Reserve.SetData(_rowIndex, COL_DOSFROM, Convert.ToInt64(_dtReserves.Rows[i]["nDOSFrom"]));
                        c1Reserve.SetData(_rowIndex, COL_DOSTO, Convert.ToString(_dtReserves.Rows[i]["nDOSTo"]));
                        c1Reserve.SetData(_rowIndex, COL_PATIENTID, Convert.ToString(_dtReserves.Rows[i]["nPatientID"]));
                        c1Reserve.SetData(_rowIndex, COL_SOURCE, Convert.ToString(_dtReserves.Rows[i]["PatientName"]));//Patient or Insurance Name
                        c1Reserve.SetData(_rowIndex, COL_COMPANYNAME, Convert.ToString(_dtReserves.Rows[i]["InsuarnceCompanyName"]));
                        string _originalPayment = "";
                        _originalPayment = ((EOBPaymentMode)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])).ToString() + "# " + Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]) + " " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dtReserves.Rows[i]["nCheckDate"])).ToString("MM/dd/yyyy") + " $ " + Convert.ToDecimal(_dtReserves.Rows[i]["nCheckAmount"]);
                        c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                        c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]));
                        c1Reserve.SetData(_rowIndex, COL_TYPE, ((EOBPaymentSubType)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                        c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]));//Note
                        c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(_dtReserves.Rows[i]["UsedReserve"]));//Used amount
                        c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount
                        c1Reserve.SetData(_rowIndex, COL_REFUND, 0);//Current amount to use from avaiable amount

                        c1Reserve.SetData(_rowIndex, COL_PAYMODE, ((EOBPaymentMode)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])));
                        c1Reserve.SetData(_rowIndex, COL_REFEOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentID"]));
                        c1Reserve.SetData(_rowIndex, COL_REFEOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nRefEOBPaymentDetailID"]));
                        c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentID"]));
                        c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nEOBPaymentDetailID"]));
                        c1Reserve.SetData(_rowIndex, COL_ACCOUNTID, Convert.ToInt64(_dtReserves.Rows[i]["nAccountID"]));
                        c1Reserve.SetData(_rowIndex, COL_ACCOUNTTYPE, Convert.ToInt32(_dtReserves.Rows[i]["nAccountType"]));
                        c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTID, Convert.ToInt64(_dtReserves.Rows[i]["nMSTAccountID"]));
                        c1Reserve.SetData(_rowIndex, COL_MSTACCOUNTTYPE, Convert.ToInt32(_dtReserves.Rows[i]["nMSTAccountType"]));

                        c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYID, Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentID"]));
                        c1Reserve.SetData(_rowIndex, COL_RES_EOBPAYDTLID, Convert.ToInt64(_dtReserves.Rows[i]["nResEOBPaymentDetailID"]));
                        c1Reserve.SetData(_rowIndex, COL_PAYMENTCLOSEDATE, gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(_dtReserves.Rows[i]["nCloseDate"])));
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
                    DesignTotalReserveGrid();
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
        private void FillPatientPayment_Reserves_Refunds()
        {
            object _TotalRefund = null;
            decimal _totalrefund = 0;
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, _nSelectedPatientId,_nPAccountId, _nClinicID);
            DBLayer oDB = new DBLayer(this._sqlDatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            DataSet dsPayment = new DataSet();
            DataSet dsReserves = new DataSet();
            DataTable dtPatientRefund = new DataTable();

            try
            {
                #region "Payment"

                objPatFinacialView.GetPatientPayment(oParameters, oDB, out dsPayment);
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
                    setGridStyle(c1FlexGridPmntTotal, c1FlexGridPmntTotal.Rows.Count - 1, 8, 14);
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
                
                #region "Reserves"
                objPatFinacialView.GetPatientReserve(oParameters, oDB, out dsReserves);

                this.c1FlexGridAvailResrv.DataMember = "Reserves";
                this.c1FlexGridAvailResrv.DataSource = dsReserves;
                c1FlexGridAvailResrv.ShowCellLabels = false;

                if (dsReserves.Tables["Reserves"].Rows.Count > 0)
                {
                    tsb_PatRefund.Enabled = true;
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

                #region "Refund"

                dtPatientRefund = objPatFinacialView.FillPatRefund();
                if (dtPatientRefund.Rows.Count > 0)
                {
                    tsb_ViewPatRefund.Enabled = true;
                }
                //Sum of the Refund amount.
                _TotalRefund = dtPatientRefund.Compute("SUM(nRefundAmount)", String.Empty);
                if (_TotalRefund != null && _TotalRefund.ToString() != "")
                    _totalrefund = Convert.ToDecimal(_TotalRefund);

                c1PatientRefund.DataSource = dtPatientRefund.DefaultView;
                DesignRefundgrid(_totalrefund);
                #endregion
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            { 
                if (_TotalRefund != null)
                    _TotalRefund = null;
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        }

        private void FillClaim_Charges(short sZeroFlag,int sSort)
        {

            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString,_nSelectedPatientId,_nPAccountId,_nClinicID);
            DataSet dsClaims_Charges = new DataSet();
            int cntr;

            try
            {
                objPatFinacialView.GetClaimsNCharges(sZeroFlag, sSort, out dsClaims_Charges);
                this.c1FlexGridChargesClaims.DataMember = "Claims_Charges";
                this.c1FlexGridChargesClaims.DataSource = dsClaims_Charges;
                FillClaimOnHold();

                if (dsClaims_Charges.Tables["Claims_Charges"].Rows.Count > 0)
                {
                    tsbViewHistory.Visible = true;
                    for (cntr = 0; cntr <= (dsClaims_Charges.Tables["Claims_Charges"].Rows.Count - 1); cntr++)
                    {
                        if (Convert.ToBoolean(dsClaims_Charges.Tables["Claims_Charges"].Rows[cntr]["blnNoteFlag"] == DBNull.Value ? 0 : dsClaims_Charges.Tables["Claims_Charges"].Rows[cntr]["blnNoteFlag"]))
                        {
                            System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Notes;
                            this.c1FlexGridChargesClaims.SetCellImage(cntr + 1, COL_NOTE_IMAGE, imgFlag);
                        }
                        if (dsClaims_Charges.Tables["Claims_Charges"].Rows.Count >= iChargesSelRow)
                            c1FlexGridChargesClaims.Row = iChargesSelRow;

                    }

                    this.c1FlexGridTotalChargesClaims.Rows[0].Visible = false;
                    this.c1FlexGridTotalChargesClaims.DataMember = "TotalClaims_Charges";
                    this.c1FlexGridTotalChargesClaims.DataSource = dsClaims_Charges;
                    setGridStyle(c1FlexGridTotalChargesClaims, c1FlexGridTotalChargesClaims.Rows.Count - 1, 12, c1FlexGridTotalChargesClaims.Cols.Count - 1);
                }
                else
                {

                    tsbViewHistory.Visible = false;
                    this.c1FlexGridTotalChargesClaims.DataMember = "Claims_Charges";
                    this.c1FlexGridTotalChargesClaims.DataSource = dsClaims_Charges;
                    this.c1FlexGridTotalChargesClaims.Rows[0].Visible = false;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);               
                this.c1FlexGridTotalChargesClaims.Rows[0].Visible = false;
            }
            finally
            {  
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        
        }

        private void FillChronology()
        {
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, _nPatientID, _nClinicID);
            DBLayer oDB = new DBLayer(this._sqlDatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            DataSet dsChronology = new DataSet();
            try
            {
                objPatFinacialView.GetChronology(oParameters, oDB, out dsChronology);
                this.c1FlexGridChronology.DataMember = "Chronology";
                this.c1FlexGridChronology.DataSource = dsChronology;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            { 
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        
        }

        private void FillStatement()
        {
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, _nSelectedPatientId,_nPAccountId, _nClinicID);
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
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);                
            }
            finally
            {
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        }

        private void FillSummary()
        {
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, _nSelectedPatientId ,_nPAccountId , _nClinicID);
            DBLayer oDB = new DBLayer(this._sqlDatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            DataSet dsSummary = new DataSet();
            try
            {

                objPatFinacialView.GetSummary(this._AgingSortByInSummary, out dsSummary);

                
                this.c1FlexGridSummary.DataMember = "Summary";
                this.c1FlexGridSummary.DataSource = dsSummary;

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
                        csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }

                }
                catch
                {
                    csClaimRowStyle = c1FlexGridSummary.Styles.Add("cs_SummaryRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                }
   


                //if (c1FlexGridSummary.Rows.Count > 1)
                //{
                //    setGridStyle(c1FlexGridSummary, c1FlexGridSummary.Rows.Count - 1, 0, 8);

                //    c1FlexGridSummary.Row = iSummarySelRow;
                //}

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
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
               
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        
        }

        private void GenerateReport()
        {
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, _nPatientID, _nClinicID);
            
            //RETRIVING REPORT SERVER NAME
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_sqlDatabaseConnectionString);
            object oValue = new object();
            oSetting.GetSetting("ReportServer", out oValue);
            if (oValue != null)
            {
                strReportServer = oValue.ToString();
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

            string reportParam = "&suser=" + _strUserName + "&Practice=" + objPatFinacialView.getClinicName();// +"&Conn=" + _sqlDatabaseConnectionString;
            //this.Text = _reportTitle;

            List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();

            SSRSReportURL = new Uri("http://" + strReportServer + "/" + strVirtualDir);

            PAHreportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            PAHreportViewer.ServerReport.ReportServerUrl = SSRSReportURL;

            //axWebBrowser1.Navigate("http://" + strReportServer + "/" + strVirtualDir + "?/" + strReportFolder + "/" + _reportName + reportParam + "&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=true");
            PAHreportViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + "rptPatientAccountHistory";// +reportParam;
            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _strUserName, false));
            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", objPatFinacialView.getClinicName(), false));
            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("nPatientID", _nPatientID.ToString(), false));
            //paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Conn", _sqlDatabaseConnectionString, false));
            this.PAHreportViewer.ServerReport.SetParameters(paramList);
            this.PAHreportViewer.RefreshReport();
        
        }

        private void OpenReserveForModify(int RowIndex)
        {
            Int64 nEobPaymentId = 0;
            Int64 nPatientId = 0;
            Int64 nEobPayDetailId = 0;

            try
            {
                if (c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentID"].Index) != null && Convert.ToString(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentID"].Index)).Trim() != "")
                { nEobPaymentId = Convert.ToInt64(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentID"].Index)); }

                if (c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentDetailID"].Index) != null && Convert.ToString(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentDetailID"].Index)).Trim() != "")
                { nEobPayDetailId = Convert.ToInt64(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nEOBPaymentDetailID"].Index)); }

                if (c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nPatientID"].Index) != null && Convert.ToString(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nPatientID"].Index)).Trim() != "")
                { nPatientId = Convert.ToInt64(c1FlexGridAvailResrv.GetData(RowIndex, c1FlexGridAvailResrv.Cols["nPatientID"].Index)); }

                frmPaymentReserveRemaning ofrmPaymentReserveRemaning = new frmPaymentReserveRemaning(_sqlDatabaseConnectionString, nPatientId, nEobPaymentId, nEobPayDetailId, true);
                //Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
                ofrmPaymentReserveRemaning.PAccountID = _nPAccountId;
                ofrmPaymentReserveRemaning.ShowDialog(this);
                if (ofrmPaymentReserveRemaning.DialogResult == DialogResult.OK)
                {
                    //FillReserves();
                    tbPatientFinancial_SelectedIndexChanged(null, null);
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

        //Changed By Shweta 20112406
        private void FillClaimOnHold()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            PatientFinancialView objPatFinacialView = new PatientFinancialView(_sqlDatabaseConnectionString, _nPatientID, _nClinicID);
            try
            {
                DataTable dtClaimOnHold = new DataTable();
                objPatFinacialView.FillClaimOnHold(oParameters, oDB, out dtClaimOnHold);

                if (dtClaimOnHold.Rows.Count > 0)
                {
                    string[] strArrClaims = dtClaimOnHold.Rows[0]["ClaimNo"].ToString().Split(',');
                    for (int iClaimCount = 0; iClaimCount <= strArrClaims.Length - 1; iClaimCount++)
                    {
                        for (int iGridClaimCount = 1; iGridClaimCount <= c1FlexGridChargesClaims.Rows.Count - 1; iGridClaimCount++)
                        {
                            if (c1FlexGridChargesClaims.GetData(iGridClaimCount, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString() == strArrClaims[iClaimCount].ToString())
                            {

                                CellStyle csSubCol;

                                //csSubCol = c1FlexGridChargesClaims.Styles.Add("SubCol");
                                try
                                {
                                    if (c1FlexGridChargesClaims.Styles.Contains("SubCol"))
                                    {
                                        csSubCol = c1FlexGridChargesClaims.Styles["SubCol"];
                                    }
                                    else
                                    {
                                        csSubCol = c1FlexGridChargesClaims.Styles.Add("SubCol");
                                        csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                                        csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                                        csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                        csSubCol.TextEffect = TextEffectEnum.Flat;
                                        csSubCol.ForeColor = Color.Red;
                                    }

                                }
                                catch
                                {
                                    csSubCol = c1FlexGridChargesClaims.Styles.Add("SubCol");
                                    csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                                    csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                                    csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    csSubCol.TextEffect = TextEffectEnum.Flat;
                                    csSubCol.ForeColor = Color.Red;
                                }
   
   
                                
                                c1FlexGridChargesClaims.Rows[iGridClaimCount].Style = csSubCol;

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();
                if (objPatFinacialView != null)
                { objPatFinacialView.Dispose(); }
            }
        }

        private void LoadInsuranceRefundLog()
        {
            DataTable _dtRefundLog = new DataTable();
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DesignRefundGrid();
                _dtRefundLog = EOBPayment.gloEOBPaymentPatient.GetPatientPaymentRefundLog(_nPatientID, _nClinicID);
                if (_dtRefundLog != null && _dtRefundLog.Rows.Count > 0)
                {
                    decimal _dTotalRefundAmount = 0;
                    foreach (DataRow row in _dtRefundLog.Rows)
                    {
                        c1InsuranceRefundLog.Rows.Add();
                        int i = c1InsuranceRefundLog.Rows.Count - 1;
                        c1InsuranceRefundLog.SetData(i, COL_CLOSEDATE_REF, row["CloseDate"]);
                        c1InsuranceRefundLog.SetData(i, COL_TRAY_REF, row["Tray"]);
                        c1InsuranceRefundLog.SetData(i, COL_COMPANY_REF, row["Company"]);
                        c1InsuranceRefundLog.SetData(i, COL_CHECK_NO_REF, row["CheckNumber"]);
                        c1InsuranceRefundLog.SetData(i, COL_REFUND_DATE_REF, row["PaymentDate"]);
                        c1InsuranceRefundLog.SetData(i, COL_REFUND_AMOUNT_REF, row["Amount"]);
                        c1InsuranceRefundLog.SetData(i, COL_NOTE_REF, row["sNoteDescription"]);
                        c1InsuranceRefundLog.SetData(i, COL_USER_REF, row["User Name"]);
                        c1InsuranceRefundLog.SetData(i, COL_EOBPAYMENT_ID_REF, row["nEOBPaymentID"]);
                        c1InsuranceRefundLog.SetData(i, COL_STATUS_REF, row["Status"]);
                        c1InsuranceRefundLog.SetData(i, COL_DATETIME_REF, row["RefundDateTime"]);
                        c1InsuranceRefundLog.SetData(i, COL_REFUNDID_REF, row["nRefundId"]);
                        c1InsuranceRefundLog.SetData(i, COL_COMPANY_ID_REF, row["nPayerID"]);
                        c1InsuranceRefundLog.SetData(i, COL_CLAIMNO_REF, row["Claim"]);
                        c1InsuranceRefundLog.SetData(i, COL_PATIENTNAME_REF, row["Patient"]);
                        _dTotalRefundAmount = _dTotalRefundAmount + Convert.ToDecimal(row["Amount"]);
                    }
                    DesignTotalRefundGrid();
                    c1InsRefundTotal.SetData(0, COL_CLOSEDATE_REF, "Total :");
                    c1InsRefundTotal.SetData(0, COL_REFUND_AMOUNT_REF, "$" + _dTotalRefundAmount);
                    c1InsRefundTotal.Width = c1InsuranceRefundLog.Width;
                    setGridStyle(c1InsRefundTotal, 0, COL_CLOSEDATE_REF, c1InsRefundTotal.Cols.Count - 1);
                }
                else
                {
                    c1InsRefundTotal.Rows.Count = 0;
                }



            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                this.Cursor = Cursors.Default;
                _dtRefundLog.Dispose();
            }
        }

        private void ViewPatientStatementTemplate()
        {
            frmWd_PatientTemplate frm = new frmWd_PatientTemplate(_sqlDatabaseConnectionString, _nTrasactionID, true);
            frm.MdiParent = this.ParentForm;
            frm.IsView = true;
            frm.Show();
            frm.WindowState = FormWindowState.Maximized;
        }

        #region "Design Grid"

        private void DesignReserveGrid()
        {
            try
            {
                //_IsFormLoading = true;
                c1Reserve.Redraw = false;
                c1Reserve.AllowSorting = AllowSortingEnum.None;

                c1Reserve.Clear();
                c1Reserve.Cols.Count = COL_COUNT;
                c1Reserve.Rows.Count = 1;
                c1Reserve.Rows.Fixed = 1;
                c1Reserve.Cols.Fixed = 0;

                #region " Set Headers "

                c1Reserve.SetData(0, COL_EOBPAYMENTID, "EOBPaymentID");
                c1Reserve.SetData(0, COL_EOBID, "EOBID");
                c1Reserve.SetData(0, COL_EOBDTLID, "EOBDetailID");
                c1Reserve.SetData(0, COL_EOBPAYMENTDTLID, "EOBPaymentDetailID");
                c1Reserve.SetData(0, COL_BLTRANSACTIONID, "BillingTransactioID");
                c1Reserve.SetData(0, COL_BLTRANDTLID, "BillingTransactioDetailID");
                c1Reserve.SetData(0, COL_BLTRANLINEID, "BillingTransactioLineID");
                c1Reserve.SetData(0, COL_DOSFROM, "DOSFrom");
                c1Reserve.SetData(0, COL_DOSTO, "DOSTo");
                c1Reserve.SetData(0, COL_PATIENTID, "PatientID");
                c1Reserve.SetData(0, COL_SOURCE, "Source");//Patient or Insurance Name
                c1Reserve.SetData(0, COL_COMPANYNAME, "Insurance Company");
                c1Reserve.SetData(0, COL_ORIGINALPAYMENT, "Original Payment");//Check Number,Date,Amount         
                c1Reserve.SetData(0, COL_USERNAME, "User");
                c1Reserve.SetData(0, COL_TORESERVES, "To Reserves");//Amount for reserve
                c1Reserve.SetData(0, COL_TYPE, "Type");//Copay,Advance,Other
                c1Reserve.SetData(0, COL_NOTE, "Note");//Note

                c1Reserve.SetData(0, COL_AVAILABLE, "Available");//Available amount
                c1Reserve.SetData(0, COL_USERESERVE, "Used");//Used Reserve
                c1Reserve.SetData(0, COL_REFUND, "Refund");//Current amount to use from avaiable amount

                c1Reserve.SetData(0, COL_PAYMODE, "Payment Mode");
                c1Reserve.SetData(0, COL_REFEOBPAYID, "Ref.EOBID");
                c1Reserve.SetData(0, COL_REFEOBPAYDTLID, "Ref.EOBDetailID");
                c1Reserve.SetData(0, COL_ACCOUNTID, "AccountID");
                c1Reserve.SetData(0, COL_ACCOUNTTYPE, "Account Type");
                c1Reserve.SetData(0, COL_MSTACCOUNTID, "Mst.AccountID");
                c1Reserve.SetData(0, COL_MSTACCOUNTTYPE, "Mst.AccountType");
                c1Reserve.SetData(0, COL_RES_EOBPAYID, "ReserveRefPayID");
                c1Reserve.SetData(0, COL_RES_EOBPAYDTLID, "ReserveRefPayDtlID");
                c1Reserve.SetData(0, COL_PAYMENTCLOSEDATE, "Close Date");
                c1Reserve.SetData(0, COL_PAYMENTMODE, "sPaymentMode");
                c1Reserve.SetData(0, COL_PAYMENTMODENO, "sPaymentNo");
                c1Reserve.SetData(0, COL_ASSO_PATIENTID, "PatientID");
                c1Reserve.SetData(0, COL_ASSO_PATIENTNAME, "Patient");
                c1Reserve.SetData(0, COL_ASSO_MSTTRANSACTIONID, "nTransactionID");
                c1Reserve.SetData(0, COL_ASSO_TRACKTRANSACTIONID, "nTrackTrnID");
                c1Reserve.SetData(0, COL_ASSO_CLAIMNO, "Claim #");

                #endregion

                #region " Show/Hide "

                c1Reserve.Cols[COL_SOURCE].Visible = true;
                c1Reserve.Cols[COL_ORIGINALPAYMENT].Visible = true;
                c1Reserve.Cols[COL_TORESERVES].Visible = true;
                c1Reserve.Cols[COL_TYPE].Visible = true;
                c1Reserve.Cols[COL_NOTE].Visible = true;
                c1Reserve.Cols[COL_AVAILABLE].Visible = true;
                c1Reserve.Cols[COL_REFUND].Visible = true;

                c1Reserve.Cols[COL_EOBPAYMENTID].Visible = false;// 0;
                c1Reserve.Cols[COL_EOBID].Visible = false;// 0;
                c1Reserve.Cols[COL_EOBDTLID].Visible = false;// 0;
                c1Reserve.Cols[COL_EOBPAYMENTDTLID].Visible = false;// 0;
                c1Reserve.Cols[COL_BLTRANSACTIONID].Visible = false;// 0;
                c1Reserve.Cols[COL_BLTRANDTLID].Visible = false;// 0;
                c1Reserve.Cols[COL_BLTRANLINEID].Visible = false;// 0;
                c1Reserve.Cols[COL_DOSFROM].Visible = false;// 50;
                c1Reserve.Cols[COL_DOSTO].Visible = false;// 0;
                c1Reserve.Cols[COL_PATIENTID].Visible = false;// 0;
                c1Reserve.Cols[COL_SOURCE].Visible = true;// 100;
                c1Reserve.Cols[COL_COMPANYNAME].Visible = true;
                c1Reserve.Cols[COL_ORIGINALPAYMENT].Visible = true;// 100;                
                c1Reserve.Cols[COL_USERNAME].Visible = true;// 100;
                c1Reserve.Cols[COL_TORESERVES].Visible = true;// 100;
                c1Reserve.Cols[COL_TYPE].Visible = false;// 100;
                c1Reserve.Cols[COL_NOTE].Visible = true;// 100;
                c1Reserve.Cols[COL_AVAILABLE].Visible = true;// 100;
                c1Reserve.Cols[COL_REFUND].Visible = false;// 100;
                c1Reserve.Cols[COL_PAYMODE].Visible = false;// 100;
                c1Reserve.Cols[COL_REFEOBPAYID].Visible = false;// 0;
                c1Reserve.Cols[COL_REFEOBPAYDTLID].Visible = false;// 0;
                c1Reserve.Cols[COL_ACCOUNTID].Visible = false;// 0;
                c1Reserve.Cols[COL_ACCOUNTTYPE].Visible = false;// 0;
                c1Reserve.Cols[COL_MSTACCOUNTID].Visible = false;// 0;
                c1Reserve.Cols[COL_MSTACCOUNTTYPE].Visible = false;// 0;
                c1Reserve.Cols[COL_USERESERVE].Visible = false;
                c1Reserve.Cols[COL_RES_EOBPAYID].Visible = false;
                c1Reserve.Cols[COL_RES_EOBPAYDTLID].Visible = false;
                c1Reserve.Cols[COL_PAYMENTCLOSEDATE].Visible = true;
                c1Reserve.Cols[COL_PAYMENTMODE].Visible = false;
                c1Reserve.Cols[COL_PAYMENTMODENO].Visible = false;
                c1Reserve.Cols[COL_ASSO_PATIENTID].Visible = false;
                c1Reserve.Cols[COL_ASSO_PATIENTNAME].Visible = false;
                c1Reserve.Cols[COL_ASSO_MSTTRANSACTIONID].Visible = false;
                c1Reserve.Cols[COL_ASSO_TRACKTRANSACTIONID].Visible = false;
                c1Reserve.Cols[COL_ASSO_CLAIMNO].Visible = true;

                #endregion

                #region " Width "

                bool _designWidth = false;

                if (_designWidth == false)
                {

                    c1Reserve.Cols[COL_EOBPAYMENTID].Width = 0;
                    c1Reserve.Cols[COL_EOBID].Width = 0;
                    c1Reserve.Cols[COL_EOBDTLID].Width = 0;
                    c1Reserve.Cols[COL_EOBPAYMENTDTLID].Width = 0;
                    c1Reserve.Cols[COL_BLTRANSACTIONID].Width = 0;
                    c1Reserve.Cols[COL_BLTRANDTLID].Width = 0;
                    c1Reserve.Cols[COL_BLTRANLINEID].Width = 0;
                    c1Reserve.Cols[COL_DOSFROM].Width = 50;
                    c1Reserve.Cols[COL_DOSTO].Width = 0;
                    c1Reserve.Cols[COL_PATIENTID].Width = 0;
                    c1Reserve.Cols[COL_SOURCE].Width = 0;
                    c1Reserve.Cols[COL_COMPANYNAME].Width = 150;
                    c1Reserve.Cols[COL_ORIGINALPAYMENT].Width = 320;
                    c1Reserve.Cols[COL_USERNAME].Width = 150;
                    c1Reserve.Cols[COL_TORESERVES].Width = 80;
                    c1Reserve.Cols[COL_TYPE].Width = 0;
                    c1Reserve.Cols[COL_NOTE].Width = 280;
                    c1Reserve.Cols[COL_AVAILABLE].Width = 75;
                    c1Reserve.Cols[COL_REFUND].Width = 0;
                    c1Reserve.Cols[COL_PAYMODE].Width = 100;
                    c1Reserve.Cols[COL_REFEOBPAYID].Width = 0;
                    c1Reserve.Cols[COL_REFEOBPAYDTLID].Width = 0;
                    c1Reserve.Cols[COL_ACCOUNTID].Width = 0;
                    c1Reserve.Cols[COL_ACCOUNTTYPE].Width = 0;
                    c1Reserve.Cols[COL_MSTACCOUNTID].Width = 0;
                    c1Reserve.Cols[COL_MSTACCOUNTTYPE].Width = 0;
                    c1Reserve.Cols[COL_USERESERVE].Width = 0;
                    c1Reserve.Cols[COL_RES_EOBPAYID].Width = 0;
                    c1Reserve.Cols[COL_RES_EOBPAYDTLID].Width = 0;
                    c1Reserve.Cols[COL_PAYMENTCLOSEDATE].Width = 80;
                    c1Reserve.Cols[COL_PAYMENTMODE].Width = 0;
                    c1Reserve.Cols[COL_PAYMENTMODENO].Width = 0;
                    c1Reserve.Cols[COL_ASSO_PATIENTID].Width = 0;
                    c1Reserve.Cols[COL_ASSO_PATIENTNAME].Width = 0;
                    c1Reserve.Cols[COL_ASSO_MSTTRANSACTIONID].Width = 0;
                    c1Reserve.Cols[COL_ASSO_TRACKTRANSACTIONID].Width = 0;
                    c1Reserve.Cols[COL_ASSO_CLAIMNO].Width = 100;
                }

                #endregion

                #region " Data Type "

                c1Reserve.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_EOBID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_EOBDTLID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_EOBPAYMENTDTLID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_BLTRANSACTIONID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_BLTRANDTLID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_BLTRANLINEID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_DOSFROM].DataType = typeof(System.String);
                c1Reserve.Cols[COL_DOSTO].DataType = typeof(System.String);
                c1Reserve.Cols[COL_PATIENTID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_SOURCE].DataType = typeof(System.String);
                c1Reserve.Cols[COL_COMPANYNAME].DataType = typeof(System.String);
                c1Reserve.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);              
                c1Reserve.Cols[COL_USERNAME].DataType = typeof(System.String);
                c1Reserve.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
                c1Reserve.Cols[COL_TYPE].DataType = typeof(System.String);
                c1Reserve.Cols[COL_NOTE].DataType = typeof(System.String);
                c1Reserve.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);
                c1Reserve.Cols[COL_REFUND].DataType = typeof(System.Decimal);
                c1Reserve.Cols[COL_USERESERVE].DataType = typeof(System.Decimal);
                c1Reserve.Cols[COL_PAYMODE].DataType = typeof(System.Int32);
                c1Reserve.Cols[COL_REFEOBPAYID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_REFEOBPAYDTLID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_ACCOUNTID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_ACCOUNTTYPE].DataType = typeof(System.Int32);
                c1Reserve.Cols[COL_MSTACCOUNTID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_MSTACCOUNTTYPE].DataType = typeof(System.Int32);
                c1Reserve.Cols[COL_RES_EOBPAYID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_RES_EOBPAYDTLID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_PAYMENTCLOSEDATE].DataType = typeof(System.String);
                c1Reserve.Cols[COL_PAYMENTMODE].DataType = typeof(System.String);
                c1Reserve.Cols[COL_PAYMENTMODENO].DataType = typeof(System.String);
                c1Reserve.Cols[COL_ASSO_PATIENTID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_ASSO_PATIENTNAME].DataType = typeof(System.String);
                c1Reserve.Cols[COL_ASSO_MSTTRANSACTIONID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_ASSO_TRACKTRANSACTIONID].DataType = typeof(System.Int64);
                c1Reserve.Cols[COL_ASSO_CLAIMNO].DataType = typeof(System.String);

                #endregion

                #region " Alignment "

                c1Reserve.Cols[COL_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_EOBID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_EOBDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_EOBPAYMENTDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_BLTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_BLTRANDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_BLTRANLINEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_DOSFROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_DOSTO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_COMPANYNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_ORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;               
                c1Reserve.Cols[COL_USERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_TORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_NOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_AVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_REFUND].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_USERESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_PAYMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_REFEOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_REFEOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_ACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_ACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_MSTACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_MSTACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_RES_EOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_RES_EOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_PAYMENTCLOSEDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_PAYMENTMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_PAYMENTMODENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Reserve.Cols[COL_ASSO_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_ASSO_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_ASSO_MSTTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_ASSO_TRACKTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Reserve.Cols[COL_ASSO_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1Reserve.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1Reserve.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1Reserve.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1Reserve.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        //csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                    }

                }
                catch
                {
                    csCurrencyStyle = c1Reserve.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    //csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
   
   
                C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;// = c1Reserve.Styles.Add("cs_EditableCurrencyStyle");
                try
                {
                    if (c1Reserve.Styles.Contains("cs_EditableCurrencyStyle"))
                    {
                        csEditableCurrencyStyle = c1Reserve.Styles["cs_EditableCurrencyStyle"];
                    }
                    else
                    {
                        csEditableCurrencyStyle = c1Reserve.Styles.Add("cs_EditableCurrencyStyle");
                        csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csEditableCurrencyStyle.Format = "c";
                        //csEditableCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        //csEditableCurrencyStyle.BackColor = Color.White;

                    }

                }
                catch
                {
                    csEditableCurrencyStyle = c1Reserve.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    //csEditableCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    //csEditableCurrencyStyle.BackColor = Color.White;

                }
       

                c1Reserve.Cols[COL_TORESERVES].Style = csCurrencyStyle;
                c1Reserve.Cols[COL_AVAILABLE].Style = csCurrencyStyle;
                c1Reserve.Cols[COL_REFUND].Style = csCurrencyStyle;
                c1Reserve.Cols[COL_USERESERVE].Style = csCurrencyStyle;

                #endregion

                c1Reserve.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1Reserve.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1Reserve.AllowEditing = false;

                c1Reserve.Cols[COL_EOBPAYMENTID].AllowEditing = false;
                c1Reserve.Cols[COL_EOBID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_EOBDTLID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_EOBPAYMENTDTLID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_BLTRANSACTIONID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_BLTRANDTLID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_BLTRANLINEID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_DOSFROM].AllowEditing = false;//50;
                c1Reserve.Cols[COL_DOSTO].AllowEditing = false;//0;
                c1Reserve.Cols[COL_PATIENTID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_SOURCE].AllowEditing = false;//100;
                c1Reserve.Cols[COL_ORIGINALPAYMENT].AllowEditing = false;//100;
                c1Reserve.Cols[COL_USERNAME].AllowEditing = false;//100;
                c1Reserve.Cols[COL_TORESERVES].AllowEditing = false;//100;
                c1Reserve.Cols[COL_TYPE].AllowEditing = false;//100;
                c1Reserve.Cols[COL_NOTE].AllowEditing = false;//100;
                c1Reserve.Cols[COL_AVAILABLE].AllowEditing = false;//100;
                c1Reserve.Cols[COL_REFUND].AllowEditing = false;//100;
                c1Reserve.Cols[COL_USERESERVE].AllowEditing = false;//100;
                c1Reserve.Cols[COL_PAYMODE].AllowEditing = false;//100;
                c1Reserve.Cols[COL_REFEOBPAYID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_REFEOBPAYDTLID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_ACCOUNTID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_ACCOUNTTYPE].AllowEditing = false;//0;
                c1Reserve.Cols[COL_MSTACCOUNTID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_MSTACCOUNTTYPE].AllowEditing = false;//0;
                c1Reserve.Cols[COL_RES_EOBPAYID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_RES_EOBPAYDTLID].AllowEditing = false;//0;
                c1Reserve.Cols[COL_PAYMENTCLOSEDATE].AllowEditing = false;//0;
                c1Reserve.Cols[COL_PAYMENTMODE].AllowEditing = false;//0;
                c1Reserve.Cols[COL_PAYMENTMODENO].AllowEditing = false;//0;
                c1Reserve.Cols[COL_ASSO_PATIENTID].AllowEditing = false;
                c1Reserve.Cols[COL_ASSO_PATIENTNAME].AllowEditing = false;
                c1Reserve.Cols[COL_ASSO_MSTTRANSACTIONID].AllowEditing = false;
                c1Reserve.Cols[COL_ASSO_TRACKTRANSACTIONID].AllowEditing = false;
                c1Reserve.Cols[COL_ASSO_CLAIMNO].AllowEditing = false;

                #endregion

                //c1Reserve.VisualStyle = VisualStyle.Office2007Blue;
                //c1Reserve.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                //c1Reserve.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                //c1Reserve.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);
                c1Reserve.ShowCellLabels = false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {  //_IsFormLoading = false; 
                c1Reserve.Redraw = true; }
        }

        private void DesignTotalReserveGrid()
        {
            try
            {
              
                c1InsReserveTotal.AllowSorting = AllowSortingEnum.None;

                c1InsReserveTotal.Clear();
                c1InsReserveTotal.Cols.Count = COL_COUNT;
                c1InsReserveTotal.Rows.Count = 1;
                c1InsReserveTotal.Rows.Fixed = 1;
                c1InsReserveTotal.Cols.Fixed = 0;
               

                //#region " Set Headers "

                //c1InsReserveTotal.SetData(0, COL_EOBPAYMENTID, "EOBPaymentID");
                //c1InsReserveTotal.SetData(0, COL_EOBID, "EOBID");
                //c1InsReserveTotal.SetData(0, COL_EOBDTLID, "EOBDetailID");
                //c1InsReserveTotal.SetData(0, COL_EOBPAYMENTDTLID, "EOBPaymentDetailID");
                //c1InsReserveTotal.SetData(0, COL_BLTRANSACTIONID, "BillingTransactioID");
                //c1InsReserveTotal.SetData(0, COL_BLTRANDTLID, "BillingTransactioDetailID");
                //c1InsReserveTotal.SetData(0, COL_BLTRANLINEID, "BillingTransactioLineID");
                //c1InsReserveTotal.SetData(0, COL_DOSFROM, "DOSFrom");
                //c1InsReserveTotal.SetData(0, COL_DOSTO, "DOSTo");
                //c1InsReserveTotal.SetData(0, COL_PATIENTID, "PatientID");
                //c1InsReserveTotal.SetData(0, COL_SOURCE, "Source");//Patient or Insurance Name
                //c1InsReserveTotal.SetData(0, COL_COMPANYNAME, "Insurance Company");
                //c1InsReserveTotal.SetData(0, COL_ORIGINALPAYMENT, "Original Payment");//Check Number,Date,Amount
                //c1InsReserveTotal.SetData(0, COL_TORESERVES, "To Reserves");//Amount for reserve
                //c1InsReserveTotal.SetData(0, COL_TYPE, "Type");//Copay,Advance,Other
                //c1InsReserveTotal.SetData(0, COL_NOTE, "Note");//Note

                //c1InsReserveTotal.SetData(0, COL_AVAILABLE, "Available");//Available amount
                //c1InsReserveTotal.SetData(0, COL_USERESERVE, "Used");//Used Reserve
                //c1InsReserveTotal.SetData(0, COL_REFUND, "Refund");//Current amount to use from avaiable amount

                //c1InsReserveTotal.SetData(0, COL_PAYMODE, "Payment Mode");
                //c1InsReserveTotal.SetData(0, COL_REFEOBPAYID, "Ref.EOBID");
                //c1InsReserveTotal.SetData(0, COL_REFEOBPAYDTLID, "Ref.EOBDetailID");
                //c1InsReserveTotal.SetData(0, COL_ACCOUNTID, "AccountID");
                //c1InsReserveTotal.SetData(0, COL_ACCOUNTTYPE, "Account Type");
                //c1InsReserveTotal.SetData(0, COL_MSTACCOUNTID, "Mst.AccountID");
                //c1InsReserveTotal.SetData(0, COL_MSTACCOUNTTYPE, "Mst.AccountType");
                //c1InsReserveTotal.SetData(0, COL_RES_EOBPAYID, "ReserveRefPayID");
                //c1InsReserveTotal.SetData(0, COL_RES_EOBPAYDTLID, "ReserveRefPayDtlID");
                //c1InsReserveTotal.SetData(0, COL_PAYMENTCLOSEDATE, "Close Date");
                //c1InsReserveTotal.SetData(0, COL_PAYMENTMODE, "sPaymentMode");
                //c1InsReserveTotal.SetData(0, COL_PAYMENTMODENO, "sPaymentNo");

                //#endregion

                #region " Show/Hide "

                c1InsReserveTotal.Cols[COL_SOURCE].Visible = true;
                c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].Visible = true;
                c1InsReserveTotal.Cols[COL_TORESERVES].Visible = true;
                c1InsReserveTotal.Cols[COL_TYPE].Visible = true;
                c1InsReserveTotal.Cols[COL_NOTE].Visible = true;
                c1InsReserveTotal.Cols[COL_AVAILABLE].Visible = true;
                c1InsReserveTotal.Cols[COL_REFUND].Visible = true;

                c1InsReserveTotal.Cols[COL_EOBPAYMENTID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_EOBID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_EOBDTLID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_EOBPAYMENTDTLID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_BLTRANSACTIONID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_BLTRANDTLID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_BLTRANLINEID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_DOSFROM].Visible = false;// 50;
                c1InsReserveTotal.Cols[COL_DOSTO].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_PATIENTID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_SOURCE].Visible = true;// 100;
                c1InsReserveTotal.Cols[COL_COMPANYNAME].Visible = true;
                c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].Visible = true;// 100;
                c1InsReserveTotal.Cols[COL_USERNAME].Visible = true;// 100;
                c1InsReserveTotal.Cols[COL_TORESERVES].Visible = true;// 100;
                c1InsReserveTotal.Cols[COL_TYPE].Visible = false;// 100;
                c1InsReserveTotal.Cols[COL_NOTE].Visible = true;// 100;
                c1InsReserveTotal.Cols[COL_AVAILABLE].Visible = true;// 100;
                c1InsReserveTotal.Cols[COL_REFUND].Visible = false;// 100;
                c1InsReserveTotal.Cols[COL_PAYMODE].Visible = false;// 100;
                c1InsReserveTotal.Cols[COL_REFEOBPAYID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_REFEOBPAYDTLID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_ACCOUNTID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_ACCOUNTTYPE].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_MSTACCOUNTID].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_MSTACCOUNTTYPE].Visible = false;// 0;
                c1InsReserveTotal.Cols[COL_USERESERVE].Visible = false;
                c1InsReserveTotal.Cols[COL_RES_EOBPAYID].Visible = false;
                c1InsReserveTotal.Cols[COL_RES_EOBPAYDTLID].Visible = false;
                c1InsReserveTotal.Cols[COL_PAYMENTCLOSEDATE].Visible = true;
                c1InsReserveTotal.Cols[COL_PAYMENTMODE].Visible = false;
                c1InsReserveTotal.Cols[COL_PAYMENTMODENO].Visible = false;

                c1InsReserveTotal.Cols[COL_ASSO_PATIENTID].Visible = false;
                c1InsReserveTotal.Cols[COL_ASSO_PATIENTNAME].Visible = false;
                c1InsReserveTotal.Cols[COL_ASSO_MSTTRANSACTIONID].Visible = false;
                c1InsReserveTotal.Cols[COL_ASSO_TRACKTRANSACTIONID].Visible = false;
                c1InsReserveTotal.Cols[COL_ASSO_CLAIMNO].Visible = true;


                #endregion

                #region " Width "

                bool _designWidth = false;

                if (_designWidth == false)
                {

                    c1InsReserveTotal.Cols[COL_EOBPAYMENTID].Width = 0;
                    c1InsReserveTotal.Cols[COL_EOBID].Width = 0;
                    c1InsReserveTotal.Cols[COL_EOBDTLID].Width = 0;
                    c1InsReserveTotal.Cols[COL_EOBPAYMENTDTLID].Width = 0;
                    c1InsReserveTotal.Cols[COL_BLTRANSACTIONID].Width = 0;
                    c1InsReserveTotal.Cols[COL_BLTRANDTLID].Width = 0;
                    c1InsReserveTotal.Cols[COL_BLTRANLINEID].Width = 0;
                    c1InsReserveTotal.Cols[COL_DOSFROM].Width = 50;
                    c1InsReserveTotal.Cols[COL_DOSTO].Width = 0;
                    c1InsReserveTotal.Cols[COL_PATIENTID].Width = 0;
                    c1InsReserveTotal.Cols[COL_SOURCE].Width = 0;
                    c1InsReserveTotal.Cols[COL_COMPANYNAME].Width = 150;
                    c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].Width = 320;
                    c1InsReserveTotal.Cols[COL_USERNAME].Width = 150;
                    c1InsReserveTotal.Cols[COL_TORESERVES].Width = 80;
                    c1InsReserveTotal.Cols[COL_TYPE].Width = 0;
                    c1InsReserveTotal.Cols[COL_NOTE].Width = 280;
                    c1InsReserveTotal.Cols[COL_AVAILABLE].Width = 75;
                    c1InsReserveTotal.Cols[COL_REFUND].Width = 0;
                    c1InsReserveTotal.Cols[COL_PAYMODE].Width = 100;
                    c1InsReserveTotal.Cols[COL_REFEOBPAYID].Width = 0;
                    c1InsReserveTotal.Cols[COL_REFEOBPAYDTLID].Width = 0;
                    c1InsReserveTotal.Cols[COL_ACCOUNTID].Width = 0;
                    c1InsReserveTotal.Cols[COL_ACCOUNTTYPE].Width = 0;
                    c1InsReserveTotal.Cols[COL_MSTACCOUNTID].Width = 0;
                    c1InsReserveTotal.Cols[COL_MSTACCOUNTTYPE].Width = 0;
                    c1InsReserveTotal.Cols[COL_USERESERVE].Width = 0;
                    c1InsReserveTotal.Cols[COL_RES_EOBPAYID].Width = 0;
                    c1InsReserveTotal.Cols[COL_RES_EOBPAYDTLID].Width = 0;
                    c1InsReserveTotal.Cols[COL_PAYMENTCLOSEDATE].Width = 80;
                    c1InsReserveTotal.Cols[COL_PAYMENTMODE].Width = 0;
                    c1InsReserveTotal.Cols[COL_PAYMENTMODENO].Width = 0;
                    c1InsReserveTotal.Cols[COL_ASSO_PATIENTID].Width = 0;
                    c1InsReserveTotal.Cols[COL_ASSO_PATIENTNAME].Width = 0;
                    c1InsReserveTotal.Cols[COL_ASSO_MSTTRANSACTIONID].Width = 0;
                    c1InsReserveTotal.Cols[COL_ASSO_TRACKTRANSACTIONID].Width = 0;
                    c1InsReserveTotal.Cols[COL_ASSO_CLAIMNO].Width = 100;
                }

                #endregion

                #region " Data Type "

                c1InsReserveTotal.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_EOBID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_EOBDTLID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_EOBPAYMENTDTLID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_BLTRANSACTIONID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_BLTRANDTLID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_BLTRANLINEID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_DOSFROM].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_DOSTO].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_PATIENTID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_SOURCE].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_COMPANYNAME].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_USERNAME].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
                c1InsReserveTotal.Cols[COL_TYPE].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_NOTE].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);
                c1InsReserveTotal.Cols[COL_REFUND].DataType = typeof(System.Decimal);
                c1InsReserveTotal.Cols[COL_USERESERVE].DataType = typeof(System.Decimal);
                c1InsReserveTotal.Cols[COL_PAYMODE].DataType = typeof(System.Int32);
                c1InsReserveTotal.Cols[COL_REFEOBPAYID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_REFEOBPAYDTLID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_ACCOUNTID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_ACCOUNTTYPE].DataType = typeof(System.Int32);
                c1InsReserveTotal.Cols[COL_MSTACCOUNTID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_MSTACCOUNTTYPE].DataType = typeof(System.Int32);
                c1InsReserveTotal.Cols[COL_RES_EOBPAYID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_RES_EOBPAYDTLID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_PAYMENTCLOSEDATE].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_PAYMENTMODE].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_PAYMENTMODENO].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_ASSO_PATIENTID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_ASSO_PATIENTNAME].DataType = typeof(System.String);
                c1InsReserveTotal.Cols[COL_ASSO_MSTTRANSACTIONID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_ASSO_TRACKTRANSACTIONID].DataType = typeof(System.Int64);
                c1InsReserveTotal.Cols[COL_ASSO_CLAIMNO].DataType = typeof(System.String);

                #endregion

                #region " Alignment "

                c1InsReserveTotal.Cols[COL_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_EOBID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_EOBDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_EOBPAYMENTDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_BLTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_BLTRANDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_BLTRANLINEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_DOSFROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_DOSTO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_COMPANYNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_USERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_TORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_NOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_AVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_REFUND].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_USERESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_PAYMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_REFEOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_REFEOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_ACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_ACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_MSTACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_MSTACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_RES_EOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_RES_EOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_PAYMENTCLOSEDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_PAYMENTMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_PAYMENTMODENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1InsReserveTotal.Cols[COL_ASSO_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_ASSO_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_ASSO_MSTTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_ASSO_TRACKTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsReserveTotal.Cols[COL_ASSO_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1InsReserveTotal.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1InsReserveTotal.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1InsReserveTotal.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1InsReserveTotal.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                        csCurrencyStyle.ForeColor = Color.Blue;

                    }

                }
                catch
                {
                    csCurrencyStyle = c1InsReserveTotal.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    csCurrencyStyle.ForeColor = Color.Blue;

                }
       

                //C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle = c1InsReserveTotal.Styles.Add("cs_EditableCurrencyStyle");
                //csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                //csEditableCurrencyStyle.Format = "c";
                //csEditableCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //csEditableCurrencyStyle.BackColor = Color.White;
              

                c1InsReserveTotal.Cols[COL_TORESERVES].Style = csCurrencyStyle;
                c1InsReserveTotal.Cols[COL_AVAILABLE].Style = csCurrencyStyle;
                c1InsReserveTotal.Cols[COL_REFUND].Style = csCurrencyStyle;
                c1InsReserveTotal.Cols[COL_USERESERVE].Style = csCurrencyStyle;

                #endregion

                c1InsReserveTotal.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                //c1InsReserveTotal.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1InsReserveTotal.AllowEditing = false;

                c1InsReserveTotal.Cols[COL_EOBPAYMENTID].AllowEditing = false;
                c1InsReserveTotal.Cols[COL_EOBID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_EOBDTLID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_EOBPAYMENTDTLID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_BLTRANSACTIONID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_BLTRANDTLID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_BLTRANLINEID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_DOSFROM].AllowEditing = false;//50;
                c1InsReserveTotal.Cols[COL_DOSTO].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_PATIENTID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_SOURCE].AllowEditing = false;//100;
                c1InsReserveTotal.Cols[COL_ORIGINALPAYMENT].AllowEditing = false;//100;
                c1InsReserveTotal.Cols[COL_USERNAME].AllowEditing = false;//100;
                c1InsReserveTotal.Cols[COL_TORESERVES].AllowEditing = false;//100;
                c1InsReserveTotal.Cols[COL_TYPE].AllowEditing = false;//100;
                c1InsReserveTotal.Cols[COL_NOTE].AllowEditing = false;//100;
                c1InsReserveTotal.Cols[COL_AVAILABLE].AllowEditing = false;//100;
                c1InsReserveTotal.Cols[COL_REFUND].AllowEditing = false;//100;
                c1InsReserveTotal.Cols[COL_USERESERVE].AllowEditing = false;//100;
                c1InsReserveTotal.Cols[COL_PAYMODE].AllowEditing = false;//100;
                c1InsReserveTotal.Cols[COL_REFEOBPAYID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_REFEOBPAYDTLID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_ACCOUNTID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_ACCOUNTTYPE].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_MSTACCOUNTID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_MSTACCOUNTTYPE].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_RES_EOBPAYID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_RES_EOBPAYDTLID].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_PAYMENTCLOSEDATE].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_PAYMENTMODE].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_PAYMENTMODENO].AllowEditing = false;//0;
                c1InsReserveTotal.Cols[COL_ASSO_PATIENTID].AllowEditing = false;
                c1InsReserveTotal.Cols[COL_ASSO_PATIENTNAME].AllowEditing = false;
                c1InsReserveTotal.Cols[COL_ASSO_MSTTRANSACTIONID].AllowEditing = false;
                c1InsReserveTotal.Cols[COL_ASSO_TRACKTRANSACTIONID].AllowEditing = false;
                c1InsReserveTotal.Cols[COL_ASSO_CLAIMNO].AllowEditing = false;


                #endregion

                c1InsReserveTotal.ShowCellLabels = false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {// _IsFormLoading = false; 
                c1InsReserveTotal.Redraw = true; }
        }

        private void DesignRefundGrid()
        {
       
            try
            {
                #region " Grid Settings "

                c1InsuranceRefundLog.Redraw = false;
                c1InsuranceRefundLog.Clear();

                c1InsuranceRefundLog.Cols.Count = COL_COUNT_REF;
                c1InsuranceRefundLog.Rows.Count = 1;
                c1InsuranceRefundLog.Rows.Fixed = 1;
                c1InsuranceRefundLog.Cols.Fixed = 0;

                c1InsuranceRefundLog.AllowEditing = false;
                c1InsuranceRefundLog.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1InsuranceRefundLog.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #endregion

                #region " Set Headers "

                c1InsuranceRefundLog.SetData(0, COL_CLOSEDATE_REF, "Close Date");
                c1InsuranceRefundLog.SetData(0, COL_TRAY_REF, "Tray");
                c1InsuranceRefundLog.SetData(0, COL_COMPANY_REF, "Refund To");
                c1InsuranceRefundLog.SetData(0, COL_CHECK_NO_REF, "Refund Check#");
                c1InsuranceRefundLog.SetData(0, COL_REFUND_DATE_REF, "Refund Date");
                c1InsuranceRefundLog.SetData(0, COL_REFUND_AMOUNT_REF, "Amount");
                c1InsuranceRefundLog.SetData(0, COL_USER_REF, "User");
                c1InsuranceRefundLog.SetData(0, COL_NOTE_REF, "Note");
                c1InsuranceRefundLog.SetData(0, COL_DATETIME_REF, "Date / Time");
                c1InsuranceRefundLog.SetData(0, COL_STATUS_REF, "Status");
                c1InsuranceRefundLog.SetData(0, COL_REFUNDID_REF, "nRefundID");
                c1InsuranceRefundLog.SetData(0, COL_CLAIMNO_REF, "Claim #");
                c1InsuranceRefundLog.SetData(0, COL_PATIENTNAME_REF, "nRefundID");

                #endregion

                #region " Show/Hide "

                c1InsuranceRefundLog.Cols[COL_EOBPAYMENT_ID_REF].Visible = false;
                c1InsuranceRefundLog.Cols[COL_COMPANY_ID_REF].Visible = false;
                c1InsuranceRefundLog.Cols[COL_PAYMENT_TRAY_ID_REF].Visible = false;
                c1InsuranceRefundLog.Cols[COL_USER_ID_REF].Visible = false;
                c1InsuranceRefundLog.Cols[COL_DEBIT_AMOUNT_REF].Visible = false;
                c1InsuranceRefundLog.Cols[COL_REFUNDID_REF].Visible = false;
                c1InsuranceRefundLog.Cols[COL_DATETIME_REF].Visible = false;
                c1InsuranceRefundLog.Cols[COL_PATIENTNAME_REF].Visible = false;

                #endregion

                #region " Width "

                c1InsuranceRefundLog.Cols[COL_CLOSEDATE_REF].Width = 75;
                c1InsuranceRefundLog.Cols[COL_TRAY_REF].Width = 90;
                c1InsuranceRefundLog.Cols[COL_COMPANY_REF].Width = 150;
                c1InsuranceRefundLog.Cols[COL_COMPANY_ID_REF].Width = 0;
                c1InsuranceRefundLog.Cols[COL_CHECK_NO_REF].Width = 100;
                c1InsuranceRefundLog.Cols[COL_REFUND_DATE_REF].Width = 90;
                c1InsuranceRefundLog.Cols[COL_REFUND_AMOUNT_REF].Width = 90;
                c1InsuranceRefundLog.Cols[COL_USER_REF].Width = 85;
                c1InsuranceRefundLog.Cols[COL_NOTE_REF].Width = 210;
                c1InsuranceRefundLog.Cols[COL_STATUS_REF].Width = 100;
                c1InsuranceRefundLog.Cols[COL_DATETIME_REF].Width = 0;
                c1InsuranceRefundLog.Cols[COL_CLAIMNO_REF].Width = 100;
                c1InsuranceRefundLog.Cols[COL_PATIENTNAME_REF].Width = 0;

                #endregion

                #region " Data Type "

                c1InsuranceRefundLog.Cols[COL_CLOSEDATE_REF].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_TRAY_REF].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_COMPANY_REF].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_CHECK_NO_REF].DataType = typeof(System.String);

                c1InsuranceRefundLog.Cols[COL_REFUND_DATE_REF].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_NOTE_REF].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_STATUS_REF].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_DATETIME_REF].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_REFUNDID_REF].DataType = typeof(System.Object);
                c1InsuranceRefundLog.Cols[COL_CLAIMNO_REF].DataType = typeof(System.String);
                c1InsuranceRefundLog.Cols[COL_PATIENTNAME_REF].DataType = typeof(System.Object);

                #endregion

                #region " Alignment "

                c1InsuranceRefundLog.Cols[COL_COMPANY_REF].TextAlign = TextAlignEnum.LeftCenter;
                c1InsuranceRefundLog.Cols[COL_CHECK_NO_REF].TextAlign = TextAlignEnum.LeftCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1InsuranceRefundLog.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1InsuranceRefundLog.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1InsuranceRefundLog.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1InsuranceRefundLog.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                    }

                }
                catch
                {
                    csCurrencyStyle = c1InsuranceRefundLog.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
             

                c1InsuranceRefundLog.Cols[COL_REFUND_AMOUNT_REF].Style = csCurrencyStyle;
                csCurrencyStyle.BackColor = Color.White;
                //c1InsuranceRefundLog.Cols[COL_USER].Style = csCurrencyStyle;

                //c1InsuranceLog.KeyActionEnter = KeyActionEnum.MoveAcross;
                //c1InsuranceLog.VisualStyle = VisualStyle.Custom;
                //c1InsuranceLog.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                //c1InsuranceLog.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                //c1InsuranceLog.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                #endregion
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally
            { c1InsuranceRefundLog.Redraw = true; }
        }

         //Bind the Patient Refund
        private void DesignRefundgrid(decimal TotalRefund)
        {
            try
            {
                // gloC1FlexStyle.Style(c1PatientRefund, true );
                c1PatientRefund.ShowCellLabels = false;
                #region " Set Header "
                c1PatientRefund.Cols["nRefundID"].Caption = "RefundID";
                c1PatientRefund.Cols["sRefundTo"].Caption = "To";
                c1PatientRefund.Cols["nCloseDate"].Caption = "Close Date";
                c1PatientRefund.Cols["sPaymentTrayDescription"].Caption = "Tray";
                c1PatientRefund.Cols["nRefundDate"].Caption = "Refund Date";
                c1PatientRefund.Cols["nRefundAmount"].Caption = "Amount";
                c1PatientRefund.Cols["sRefundNotes"].Caption = "Note";
                c1PatientRefund.Cols["sUserName"].Caption = "User";
                c1PatientRefund.Cols["dtCreatedDateTime"].Caption = "Date/Time";
                c1PatientRefund.Cols["Status"].Caption = "Status";
                c1PatientRefund.Cols["sPatientName"].Caption = "Patient";

                #endregion

                int _nWidth = 0;
                _nWidth = 976;//Convert.ToInt32( c1QueuedClaims.Width);
                c1PatientRefund.Cols["nRefundID"].Width = 0;
                c1PatientRefund.Cols["nRefundID"].Visible = false;
                c1PatientRefund.Cols["nCloseDate"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1PatientRefund.Cols["sPaymentTrayDescription"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1PatientRefund.Cols["sRefundTo"].Width = Convert.ToInt32(_nWidth * 0.14);
                c1PatientRefund.Cols["nRefundDate"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1PatientRefund.Cols["nRefundAmount"].Width = Convert.ToInt32(_nWidth * 0.10);
                c1PatientRefund.Cols["sRefundNotes"].Width = Convert.ToInt32(_nWidth * 0.25);
                c1PatientRefund.Cols["sUserName"].Width = Convert.ToInt32(_nWidth * 0.11);
                c1PatientRefund.Cols["dtCreatedDateTime"].Width = Convert.ToInt32(_nWidth * 0.15);
                c1PatientRefund.Cols["dtCreatedDateTime"].Format = "MM/dd/yyyy hh:mm tt";
                c1PatientRefund.Cols["Status"].Width = Convert.ToInt32(_nWidth * 0.07);
                c1PatientRefund.Cols["nRefundAmount"].Format = "c";

                c1PatientRefund.Cols["nCloseDate"].DataType = typeof(System.DateTime);
                c1PatientRefund.Cols["nCloseDate"].Format = "MM/dd/yyyy";
                c1PatientRefund.Cols["nRefundDate"].DataType = typeof(System.DateTime);
                c1PatientRefund.Cols["nRefundDate"].Format = "MM/dd/yyyy";


                if (c1PatientRefund.Rows.Count > 1)
                {
                    c1RefundTotal.Cols[0].Width = 0;
                    c1RefundTotal.Cols[1].Width = Convert.ToInt32(_nWidth * 0.10);
                    c1RefundTotal.Cols[2].Width = Convert.ToInt32(_nWidth * 0.10);
                    c1RefundTotal.Cols[3].Width = Convert.ToInt32(_nWidth * 0.14);
                    c1RefundTotal.Cols[4].Width = Convert.ToInt32(_nWidth * 0.10);
                    c1RefundTotal.Cols[5].Width = Convert.ToInt32(_nWidth * 0.10);
                    c1RefundTotal.Cols[6].Width = Convert.ToInt32(_nWidth * 0.25);
                    c1RefundTotal.Cols[7].Width = Convert.ToInt32(_nWidth * 0.11);
                    c1RefundTotal.Cols[8].Width = Convert.ToInt32(_nWidth * 0.15);
                    c1RefundTotal.Cols[9].Width = Convert.ToInt32(_nWidth * 0.07);
                    c1RefundTotal.Cols[1].Caption = "Total :";
                    c1RefundTotal.Cols[5].Caption = "$" + Convert.ToString(TotalRefund);
                    setGridStyle(c1RefundTotal, 0, 1, 5);
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

        private void DesignTotalRefundGrid()
        {

            try
            {
                #region " Grid Settings "

                c1InsRefundTotal.Redraw = false;
                c1InsRefundTotal.Clear();

                c1InsRefundTotal.Cols.Count = COL_COUNT_REF;
                c1InsRefundTotal.Rows.Count = 1;
                c1InsRefundTotal.Rows.Fixed = 1;
                c1InsRefundTotal.Cols.Fixed = 0;

                c1InsRefundTotal.AllowEditing = false;
                c1InsRefundTotal.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                //c1InsRefundTotal.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;
                c1InsRefundTotal.AllowSorting = AllowSortingEnum.None;

                #endregion

                //#region " Set Headers "

                //c1InsRefundTotal.SetData(0, COL_CLOSEDATE_REF, "Close Date");
                //c1InsRefundTotal.SetData(0, COL_TRAY_REF, "Tray");
                //c1InsRefundTotal.SetData(0, COL_COMPANY_REF, "Refund To");
                //c1InsRefundTotal.SetData(0, COL_CHECK_NO_REF, "Refund Check#");
                //c1InsRefundTotal.SetData(0, COL_REFUND_DATE_REF, "Refund Date");
                //c1InsRefundTotal.SetData(0, COL_REFUND_AMOUNT_REF, "Amount");
                //c1InsRefundTotal.SetData(0, COL_USER_REF, "User");
                //c1InsRefundTotal.SetData(0, COL_NOTE_REF, "Note");
                //c1InsRefundTotal.SetData(0, COL_DATETIME_REF, "Date / Time");
                //c1InsRefundTotal.SetData(0, COL_STATUS_REF, "Status");
                //c1InsRefundTotal.SetData(0, COL_REFUNDID_REF, "nRefundID");

                //#endregion

                #region " Show/Hide "

                c1InsRefundTotal.Cols[COL_EOBPAYMENT_ID_REF].Visible = false;
                c1InsRefundTotal.Cols[COL_COMPANY_ID_REF].Visible = false;
                c1InsRefundTotal.Cols[COL_PAYMENT_TRAY_ID_REF].Visible = false;
                c1InsRefundTotal.Cols[COL_USER_ID_REF].Visible = false;
                c1InsRefundTotal.Cols[COL_DEBIT_AMOUNT_REF].Visible = false;
                c1InsRefundTotal.Cols[COL_REFUNDID_REF].Visible = false;
                c1InsRefundTotal.Cols[COL_DATETIME_REF].Visible = false;
                c1InsRefundTotal.Cols[COL_PATIENTNAME_REF].Visible = false;

                #endregion

                #region " Width "

                c1InsRefundTotal.Cols[COL_CLOSEDATE_REF].Width = 75;
                c1InsRefundTotal.Cols[COL_TRAY_REF].Width = 90;
                c1InsRefundTotal.Cols[COL_COMPANY_REF].Width = 150;
                c1InsRefundTotal.Cols[COL_COMPANY_ID_REF].Width = 0;
                c1InsRefundTotal.Cols[COL_CHECK_NO_REF].Width = 100;
                c1InsRefundTotal.Cols[COL_REFUND_DATE_REF].Width = 90;
                c1InsRefundTotal.Cols[COL_REFUND_AMOUNT_REF].Width = 90;
                c1InsRefundTotal.Cols[COL_USER_REF].Width = 85;
                c1InsRefundTotal.Cols[COL_NOTE_REF].Width = 210;
                c1InsRefundTotal.Cols[COL_STATUS_REF].Width = 100;
                c1InsRefundTotal.Cols[COL_DATETIME_REF].Width = 0;
                c1InsRefundTotal.Cols[COL_CLAIMNO_REF].Width = 100;
                c1InsRefundTotal.Cols[COL_PATIENTNAME_REF].Width = 0;

                #endregion

                #region " Data Type "

                c1InsRefundTotal.Cols[COL_CLOSEDATE_REF].DataType = typeof(System.String);
                c1InsRefundTotal.Cols[COL_TRAY_REF].DataType = typeof(System.String);
                c1InsRefundTotal.Cols[COL_COMPANY_REF].DataType = typeof(System.String);
                c1InsRefundTotal.Cols[COL_CHECK_NO_REF].DataType = typeof(System.String);

                c1InsRefundTotal.Cols[COL_REFUND_DATE_REF].DataType = typeof(System.String);
                c1InsRefundTotal.Cols[COL_NOTE_REF].DataType = typeof(System.String);
                c1InsRefundTotal.Cols[COL_STATUS_REF].DataType = typeof(System.String);
                c1InsRefundTotal.Cols[COL_DATETIME_REF].DataType = typeof(System.String);
                c1InsRefundTotal.Cols[COL_REFUNDID_REF].DataType = typeof(System.Object);
                c1InsRefundTotal.Cols[COL_CLAIMNO_REF].DataType = typeof(System.String);
                c1InsRefundTotal.Cols[COL_PATIENTNAME_REF].DataType = typeof(System.String);

                #endregion

                #region " Alignment "

                c1InsRefundTotal.Cols[COL_COMPANY_REF].TextAlign = TextAlignEnum.LeftCenter;
                c1InsRefundTotal.Cols[COL_CHECK_NO_REF].TextAlign = TextAlignEnum.LeftCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1InsRefundTotal.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1InsRefundTotal.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1InsRefundTotal.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1InsRefundTotal.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                        csCurrencyStyle.ForeColor = Color.Blue;

                    }

                }
                catch
                {
                    csCurrencyStyle = c1InsRefundTotal.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    csCurrencyStyle.ForeColor = Color.Blue;

                }
   

                c1InsRefundTotal.Cols[COL_REFUND_AMOUNT_REF].Style = csCurrencyStyle;
                //c1InsRefundTotal.Cols[COL_USER].Style = csCurrencyStyle;

                //c1InsuranceLog.KeyActionEnter = KeyActionEnum.MoveAcross;
                //c1InsuranceLog.VisualStyle = VisualStyle.Custom;
                //c1InsuranceLog.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                //c1InsuranceLog.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                //c1InsuranceLog.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                #endregion
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally
            { c1InsRefundTotal.Redraw = true; }
        }

        #endregion

        #region "Commented By Shweta - 20112406"
        //private long GetClaimTransactionID(Int64 _nClaimNo, string _subclaimNo, bool _isVoid)
        //{

        //    #region "To Fetch the TransactionID of Claim"

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
        //    object _TransactionId = null;
        //    string strQuery = "";
        //    DataTable _dtTransID = null;
        //    try
        //    {
        //        oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
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

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
        //    Boolean isVoided = false;
        //    DataTable _dtClaimVoided = null;
        //    try
        //    {
        //        oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
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


        //private string getClinicName()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
        //    oDB.Connect(false);
        //    object _Result = oDB.ExecuteScalar_Query("SELECT COALESCE(sClinicName,'') AS sClinicName FROM Clinic_MST WITH (NOLOCK)");
        //    if (_Result.ToString() != "")
        //    { return _Result.ToString(); }
        //    else
        //    { return ""; }
        //}


        //private Boolean chkIsInsReserveRefundExist()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
        //    DataTable _dtReserves = new DataTable();
        //    DataTable _dtRefundLog = new DataTable();
        //    Boolean _bIsDataExist = false;
        //    try
        //    {
        //        oDB.Connect(false);
        //        oDB.Retrive_Query("Select * from view_PatientInsCompanyReserves where AssociationPatientID = " + _nPatientID, out _dtReserves);
        //        oDB.Disconnect();
        //        if (_dtReserves != null && _dtReserves.Rows.Count > 0)
        //        {
        //            _bIsDataExist = true;
        //        }
        //        else
        //        {
        //            _dtRefundLog = EOBPayment.gloEOBPaymentPatient.GetPatientPaymentRefundLog(_nPatientID, _nClinicID);
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


        //private void Fill_PatientStatement(out DataSet dsPatFinView)
        //{
        //    SqlConnection _con = new SqlConnection(this._sqlDatabaseConnectionString);
        //    dsPatFinView = new DataSet();
        //    try
        //    {
        //        string sqlQuery = "";

        //        sqlQuery = "SELECT CASE BL_Batch_PatientStatement_Mst.bIsUnclosedDay  WHEN 1 THEN CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) + ' [Unclosed] '  ELSE CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) END AS dtStatementDate,Convert(varchar,BL_Batch_PatientStatement_Mst.dtCreateDate,101) As dtCreateDate,ISNULL(BL_Batch_PatientStatement_Mst.sBatchName,'') as sBatchName, " +
        //                  " ISNULL(BL_Batch_PatientStatement_Mst.sUserName,'') as sUserName, ISNULL(Patient.sFirstName,'') as sFirstName, ISNULL(Patient.sMiddleName,'') as sMiddleName, ISNULL(Patient.sLastName,'') as sLastName, " +
        //                  " ISNULL(BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID,0) as nBatchPateintStatMstID,ISNULL(BL_Batch_PatientStatement_DTL.nTempleteTransactionID,0) as nTempleteTransactionID, ISNULL(Patient.nPatientID,0) as nPatientID,sVoidNotes As Notes,CASE WHEN isnull(BL_Batch_PatientStatement_DTL.bIsVoid,0) = 0 then ' ' ELSE 'Voided' END As Status,BL_Batch_PatientStatement_DTL.nBatchPateintStatDtlID" +
        //                  " FROM Patient WITH (NOLOCK) INNER JOIN " +
        //                  " BL_Batch_PatientStatement_DTL WITH (NOLOCK) ON Patient.nPatientID = BL_Batch_PatientStatement_DTL.nPatientID INNER JOIN " +
        //                  " BL_Batch_PatientStatement_Mst WITH (NOLOCK) ON BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID = BL_Batch_PatientStatement_Mst.nBatchPateintStatMstID where Patient.nPatientID=" + _nPatientID + " order by dtCreateDate desc";
        //        SqlDataAdapter adpt = new SqlDataAdapter(sqlQuery, _con);
        //        adpt.Fill(dsPatFinView);
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


        //private int GetExcludePatientDtl()
        //{
        //    //SqlConnection _Con = new SqlConnection(this._sqlDatabaseConnectionString);
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
        //    string _sqlQuery = "";
        //    int _result;
        //    try
        //    {
        //        _sqlQuery = " SELECT Count(*) FROM PatientSettings WITH (NOLOCK) WHERE sValue = 1 AND sName = 'Exclude from Statement' AND nPatientID = " + _nPatientID + " ";
        //        oDB.Connect(false);
        //        _result = (int)oDB.ExecuteScalar_Query(_sqlQuery);
        //        oDB.Disconnect();

        //        return _result;

        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}


        //private void FillReserves()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_sqlDatabaseConnectionString);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    //DataTable _dtReserves = new DataTable();
        //    DataSet dsReserves = new DataSet();
        //    try
        //    {

        //        //DesignPaymentGrid(c1Reserve);

        //        // _IsFormLoading = true;

        //        oParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
        //        oParameters.Add("@nClinicID", _nClinicID, ParameterDirection.Input, SqlDbType.BigInt);//NUMERIC(18,0)
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

        #endregion

        #endregion

    }
}