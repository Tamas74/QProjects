using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloPatient;

namespace gloBilling
{
    public partial class frmPaymentUseReserve : Form
    {

        #region " Private Variables "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        Int64 _UserId = 0;
        string _UserName = "";
        private string _MessageBoxCaption = "";
        private bool _IsFormLoading = false;
        private Int64 _patientId = 0;

        public decimal SelectedUseReserveAmount = 0;
        public gloGeneralItem.gloItems oSeletedReserveItems = new gloGeneralItem.gloItems();
        //Added by Mahesh S(Apollo) on 10-may-2011
        private Int64 _nPAccountID = 0;
        private bool _IsPatientAccountFeature = false;

        private DateTime _closeDate = DateTime.Now;
        private string _closeDayTray = "";
        private bool _isValidResAmount = true;
        private bool _isFormClosing = false;

        #endregion " Private Variables "

        #region  " Grid Constants "

        //nEOBPaymentID, nEOBID, nEOBDtlID, nEOBPaymentDetailID, 
        //nBillingTransactionID, nBillingTransactionDetailID, nBillingTransactionLineNo, 
        //nPatientID, nDOSFrom, nDOSTo, nAmount, nPayMode, 
        //nRefEOBPaymentID, nRefEOBPaymentDetailID, 
        //nAccountID, nAccountType, nMSTAccountID, nMSTAccountType 

        const int COL_EOBPAYMENTID = 0;
        const int COL_EOBID = 1;
        const int COL_EOBDTLID = 2;
        const int COL_EOBPAYMENTDTLID = 3;
        const int COL_BLTRANSACTIONID = 4;
        const int COL_BLTRANDTLID = 5;
        const int COL_BLTRANLINEID = 6;
        const int COL_DOSFROM = 7;
        const int COL_DOSTO = 8;

        const int COL_PATIENTID = 9;
        const int COL_SOURCE = 10; //Patient or Insurance Name

        const int COL_ORIGINALPAYMENT = 11;//Check Number,Date,Amount
        const int COL_TORESERVES = 12;//Amount for reserve
        const int COL_TYPE = 13;//Copay,Advance,Other
        const int COL_NOTE = 14;//Note

        const int COL_AVAILABLE = 15;//Available amount
        const int COL_USERESERVE = 16;//Used Reserve
        const int COL_USENOW = 17;//Current amount to use from avaiable amount

        const int COL_PAYMODE = 18;
        const int COL_REFEOBPAYID = 19;
        const int COL_REFEOBPAYDTLID = 20;
        const int COL_ACCOUNTID = 21;
        const int COL_ACCOUNTTYPE = 22;
        const int COL_MSTACCOUNTID = 23;
        const int COL_MSTACCOUNTTYPE = 24;
        const int COL_RES_EOBPAYID = 25;
        const int COL_RES_EOBPAYDTLID = 26;


        const int COL_COUNT = 27;

        #endregion

        #region " Property Procedures "

        public Int64 ClinicID
        { get { return _ClinicID; } set { _ClinicID = value; } }
        public Int64 UserID
        { get { return _UserId; } set { _UserId = value; } }
        public string UserName
        { get { return _UserName; } set { _UserName = value; } }
        private Int64 PatientID
        { get { return _patientId; } set { _patientId = value; } }

        //added by mahesh s on 10-may-2011
        private Int64 PAccountID
        { get { return _nPAccountID; } set { _nPAccountID = value; } }

        public DateTime CloseDate
        {
            get { return _closeDate; }
            set { _closeDate = value; }
        }
        public string CloseDayTray
        {
            get { return _closeDayTray; }
            set { _closeDayTray = value; }
        }

        #endregion " Property Procedures "

        #region " Constructors "

        public frmPaymentUseReserve(string DatabaseConnectionString, Int64 Patientid)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _patientId = Patientid;

            #region " Retrive Clinic ID "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion " Retrive Clinic ID "

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserId = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserId = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        public frmPaymentUseReserve(string DatabaseConnectionString, Int64 Patientid, Int64 PAccountID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _nPAccountID = PAccountID;
            _patientId = Patientid;

            #region " Retrive Clinic ID "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion " Retrive Clinic ID "

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserId = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserId = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion

            //Added By Mahesh S(Apollo)
            gloAccount objAccount = new gloAccount(_databaseconnectionstring);
            _IsPatientAccountFeature = objAccount.GetPatientAccountFeatureSetting();
        }


        #endregion " Constructors "

        #region " Form Load "

        private void frmPaymentUseReserve_Load(object sender, EventArgs e)
        {

            //added by mahesh s on 10-may-2011
            if (_IsPatientAccountFeature)
            {
                _nPAccountID = PAccountID;
                FillReserves_PAF(_nPAccountID);
            }
            else
            {
                FillReserves();
            }
            if (c1Reserve.Rows.Count > 1) { c1Reserve.Focus(); c1Reserve.Select(1, COL_USENOW); }
        }

        #endregion

        #region " Form Controls events "

        private void frmPaymentUseReserve_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isFormClosing = true;
            c1Reserve.FinishEditing();
            if (this.DialogResult != DialogResult.OK) { this.DialogResult = DialogResult.Cancel; }
        }

        private void c1Reserve_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        #endregion

        #region "Toolstrip button events "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            gloGeneralItem.gloItems _oSeletedReserveItems = new gloGeneralItem.gloItems();
            gloGeneralItem.gloItem ogloItem = null;
            decimal _selectedAmount = 0;
            Int64 _selEOBPayId = 0;
            Int64 _selEOBPayDtlId = 0;

            Int64 _selresEOBPayId = 0;
            Int64 _selresEOBPayDtlId = 0;

            Int64 _selRefEOBPayId = 0;
            Int64 _selRefEOBPayDtlId = 0;
            Int32 _selEOBPayPayMode = 0;

            try
            {
                c1Reserve.FinishEditing();

                if (_isValidResAmount == false) { return; }

                if (c1Reserve.Rows.Count > 0)
                {
                    c1Reserve.FinishEditing();

                    for (int i = 1; i <= c1Reserve.Rows.Count - 1; i++)
                    {
                        if (c1Reserve.GetData(i, COL_USENOW) != null && Convert.ToString(c1Reserve.GetData(i, COL_USENOW)).ToString().Trim() != "")
                        {
                            if (Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW)) > 0)
                            {
                                _selectedAmount += Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW));

                                if (c1Reserve.GetData(i, COL_EOBPAYMENTID) != null && Convert.ToString(c1Reserve.GetData(i, COL_EOBPAYMENTID)).ToString().Trim() != "")
                                { _selEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTID)); }
                                if (c1Reserve.GetData(i, COL_EOBPAYMENTDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)).ToString().Trim() != "")
                                { _selEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)); }

                                if (c1Reserve.GetData(i, COL_RES_EOBPAYID) != null && Convert.ToString(c1Reserve.GetData(i, COL_RES_EOBPAYID)).ToString().Trim() != "")
                                { _selresEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_RES_EOBPAYID)); }
                                if (c1Reserve.GetData(i, COL_RES_EOBPAYDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_RES_EOBPAYDTLID)).ToString().Trim() != "")
                                { _selresEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_RES_EOBPAYDTLID)); }



                                if (c1Reserve.GetData(i, COL_REFEOBPAYID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYID)).ToString().Trim() != "")
                                { _selRefEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYID)); }

                                if (c1Reserve.GetData(i, COL_REFEOBPAYDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)).ToString().Trim() != "")
                                { _selRefEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)); }

                                if (c1Reserve.GetData(i, COL_PAYMODE) != null && Convert.ToString(c1Reserve.GetData(i, COL_PAYMODE)).ToString().Trim() != "")
                                {
                                    _selEOBPayPayMode = ((EOBPaymentMode)Convert.ToInt32(c1Reserve.GetData(i, COL_PAYMODE))).GetHashCode();
                                }

                                ogloItem = new gloGeneralItem.gloItem(_selEOBPayId, Convert.ToString(_selEOBPayDtlId), Convert.ToString(c1Reserve.GetData(i, COL_USENOW)).Trim());
                                ogloItem.SubItems.Add(_selRefEOBPayId, _selEOBPayPayMode.ToString(), _selRefEOBPayDtlId.ToString());
                                _oSeletedReserveItems.Add(ogloItem);
                              //  ogloItem.Dispose(); //SLR: it should not be since subitems will be disposed

                                _selEOBPayId = 0;
                                _selEOBPayDtlId = 0;
                                _selEOBPayId = 0;
                                _selEOBPayDtlId = 0;
                                _selEOBPayPayMode = 0;
                            }
                        }
                    }
                }

                if (_selectedAmount > 0)
                {
                    SelectedUseReserveAmount = _selectedAmount;
                    oSeletedReserveItems = _oSeletedReserveItems;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select the amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (c1Reserve != null && c1Reserve.Rows.Count > 1) { c1Reserve.Focus(); c1Reserve.Select(1, COL_USENOW, true); }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tsb_ShowDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1Reserve != null && c1Reserve.Rows.Count > 0)
                {
                    if (c1Reserve.RowSel > 0)
                    {
                        OpenReserveForModify(c1Reserve.RowSel);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void tsb_ShowPatRefund_Click(object sender, EventArgs e)
        {
            gloGeneralItem.gloItems _oSeletedReserveItems = new gloGeneralItem.gloItems();
            gloGeneralItem.gloItem ogloItem = null;
            decimal _selectedAmount = 0;
            Int64 _selEOBPayId = 0;
            Int64 _selEOBPayDtlId = 0;
            Int64 _selRefEOBPayId = 0;
            Int64 _selRefEOBPayDtlId = 0;
            Int32 _selEOBPayPayMode = 0;

            try
            {
                if (c1Reserve != null && c1Reserve.Rows.Count > 1)
                {
                    c1Reserve.FinishEditing();

                    for (int i = 1; i <= c1Reserve.Rows.Count - 1; i++)
                    {
                        if (c1Reserve.GetData(i, COL_USENOW) != null && Convert.ToString(c1Reserve.GetData(i, COL_USENOW)).ToString().Trim() != "")
                        {
                            if (Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW)) > 0)
                            {
                                _selectedAmount += Convert.ToDecimal(c1Reserve.GetData(i, COL_USENOW));

                                if (c1Reserve.GetData(i, COL_EOBPAYMENTID) != null && Convert.ToString(c1Reserve.GetData(i, COL_EOBPAYMENTID)).ToString().Trim() != "")
                                { _selEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTID)); }
                                if (c1Reserve.GetData(i, COL_EOBPAYMENTDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)).ToString().Trim() != "")
                                { _selEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_EOBPAYMENTDTLID)); }

                                if (c1Reserve.GetData(i, COL_REFEOBPAYID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYID)).ToString().Trim() != "")
                                { _selRefEOBPayId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYID)); }

                                if (c1Reserve.GetData(i, COL_REFEOBPAYDTLID) != null && Convert.ToString(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)).ToString().Trim() != "")
                                { _selRefEOBPayDtlId = Convert.ToInt64(c1Reserve.GetData(i, COL_REFEOBPAYDTLID)); }

                                if (c1Reserve.GetData(i, COL_PAYMODE) != null && Convert.ToString(c1Reserve.GetData(i, COL_PAYMODE)).ToString().Trim() != "")
                                {
                                    _selEOBPayPayMode = ((EOBPaymentMode)Convert.ToInt32(c1Reserve.GetData(i, COL_PAYMODE))).GetHashCode();
                                }

                                ogloItem = new gloGeneralItem.gloItem(_selEOBPayId, Convert.ToString(_selEOBPayDtlId), Convert.ToString(c1Reserve.GetData(i, COL_USENOW)).Trim());
                                ogloItem.SubItems.Add(_selRefEOBPayId, _selEOBPayPayMode.ToString(), _selRefEOBPayDtlId.ToString());
                                _oSeletedReserveItems.Add(ogloItem);
                              //  ogloItem.Dispose(); //SLR: it should not be since subitems will be disposed

                                _selEOBPayId = 0;
                                _selEOBPayDtlId = 0;
                                _selEOBPayId = 0;
                                _selEOBPayDtlId = 0;
                                _selEOBPayPayMode = 0;
                            }
                        }
                    }

                    if (_selectedAmount > 0)
                    {

                        frmBillingEOBPatientRefund ofrmBillingEOBPatientRefund = new frmBillingEOBPatientRefund(_databaseconnectionstring, _patientId);
                        ofrmBillingEOBPatientRefund.oSeletedReserveItems = _oSeletedReserveItems;
                        ofrmBillingEOBPatientRefund.RefundAmt = _selectedAmount;
                        ofrmBillingEOBPatientRefund.CloseDayTray = _closeDayTray;
                        ofrmBillingEOBPatientRefund.CloseDate = this.CloseDate;
                        ofrmBillingEOBPatientRefund.ShowDialog(this);
                        ofrmBillingEOBPatientRefund.Dispose();
                        ofrmBillingEOBPatientRefund = null;
                        FillReserves();
                        if (c1Reserve.Rows.Count > 1) { c1Reserve.Select(1, COL_USENOW, true); }
                    }
                    else
                    {
                        MessageBox.Show("Please select the amount to refund.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (c1Reserve != null && c1Reserve.Rows.Count > 1) { c1Reserve.Focus(); c1Reserve.Select(1, COL_USENOW, true); }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void OpenReserveForModify(int RowIndex)
        {
            Int64 _eobPaymentId = 0;
            Int64 _patientId = 0;
            Int64 _eobPayDetailId = 0;

            try
            {
                if (c1Reserve.GetData(RowIndex, COL_EOBPAYMENTID) != null && Convert.ToString(c1Reserve.GetData(RowIndex, COL_EOBPAYMENTID)).Trim() != "")
                { _eobPaymentId = Convert.ToInt64(c1Reserve.GetData(RowIndex, COL_EOBPAYMENTID)); }

                if (c1Reserve.GetData(RowIndex, COL_EOBPAYMENTDTLID) != null && Convert.ToString(c1Reserve.GetData(RowIndex, COL_EOBPAYMENTDTLID)).Trim() != "")
                { _eobPayDetailId = Convert.ToInt64(c1Reserve.GetData(RowIndex, COL_EOBPAYMENTDTLID)); }

                if (c1Reserve.GetData(RowIndex, COL_PATIENTID) != null && Convert.ToString(c1Reserve.GetData(RowIndex, COL_PATIENTID)).Trim() != "")
                { _patientId = Convert.ToInt64(c1Reserve.GetData(RowIndex, COL_PATIENTID)); }

                frmPaymentReserveRemaning ofrmPaymentReserveRemaning = new frmPaymentReserveRemaning(_databaseconnectionstring, _patientId, _eobPaymentId, _eobPayDetailId, true);
                ofrmPaymentReserveRemaning.PAccountID = _nPAccountID;
                ofrmPaymentReserveRemaning.ShowDialog(this);
                if (ofrmPaymentReserveRemaning.DialogResult == DialogResult.OK)
                { FillReserves(); }
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

        #endregion

        #region " Design Grid "

        private void DesignPaymentGrid(C1FlexGrid c1Payment)
        {
            try
            {
                _IsFormLoading = true;
                c1Payment.Redraw = false;
                c1Payment.AllowSorting = AllowSortingEnum.None;

                c1Payment.Clear();
                c1Payment.Cols.Count = COL_COUNT;
                c1Payment.Rows.Count = 1;
                c1Payment.Rows.Fixed = 1;
                c1Payment.Cols.Fixed = 0;

                #region " Set Headers "

                c1Payment.SetData(0, COL_EOBPAYMENTID, "EOBPaymentID");
                c1Payment.SetData(0, COL_EOBID, "EOBID");
                c1Payment.SetData(0, COL_EOBDTLID, "EOBDetailID");
                c1Payment.SetData(0, COL_EOBPAYMENTDTLID, "EOBPaymentDetailID");
                c1Payment.SetData(0, COL_BLTRANSACTIONID, "BillingTransactioID");
                c1Payment.SetData(0, COL_BLTRANDTLID, "BillingTransactioDetailID");
                c1Payment.SetData(0, COL_BLTRANLINEID, "BillingTransactioLineID");
                c1Payment.SetData(0, COL_DOSFROM, "DOSFrom");
                c1Payment.SetData(0, COL_DOSTO, "DOSTo");
                c1Payment.SetData(0, COL_PATIENTID, "PatientID");
                c1Payment.SetData(0, COL_SOURCE, "Source");//Patient or Insurance Name

                c1Payment.SetData(0, COL_ORIGINALPAYMENT, "Original Payment");//Check Number,Date,Amount
                c1Payment.SetData(0, COL_TORESERVES, "To Reserves");//Amount for reserve
                c1Payment.SetData(0, COL_TYPE, "Type");//Copay,Advance,Other
                c1Payment.SetData(0, COL_NOTE, "Note");//Note

                c1Payment.SetData(0, COL_AVAILABLE, "Available");//Available amount
                c1Payment.SetData(0, COL_USERESERVE, "Used");//Used Reserve
                c1Payment.SetData(0, COL_USENOW, "Use Now");//Current amount to use from avaiable amount

                c1Payment.SetData(0, COL_PAYMODE, "Payment Mode");
                c1Payment.SetData(0, COL_REFEOBPAYID, "Ref.EOBID");
                c1Payment.SetData(0, COL_REFEOBPAYDTLID, "Ref.EOBDetailID");
                c1Payment.SetData(0, COL_ACCOUNTID, "AccountID");
                c1Payment.SetData(0, COL_ACCOUNTTYPE, "Account Type");
                c1Payment.SetData(0, COL_MSTACCOUNTID, "Mst.AccountID");
                c1Payment.SetData(0, COL_MSTACCOUNTTYPE, "Mst.AccountType");
                c1Payment.SetData(0, COL_RES_EOBPAYID, "ReserveRefPayID");
                c1Payment.SetData(0, COL_RES_EOBPAYDTLID, "ReserveRefPayDtlID");


                #endregion

                #region " Show/Hide "

                c1Payment.Cols[COL_SOURCE].Visible = true;
                c1Payment.Cols[COL_ORIGINALPAYMENT].Visible = true;
                c1Payment.Cols[COL_TORESERVES].Visible = true;
                c1Payment.Cols[COL_TYPE].Visible = true;
                c1Payment.Cols[COL_NOTE].Visible = true;
                c1Payment.Cols[COL_AVAILABLE].Visible = true;
                c1Payment.Cols[COL_USENOW].Visible = true;

                c1Payment.Cols[COL_EOBPAYMENTID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANSACTIONID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_BLTRANLINEID].Visible = false;// 0;
                c1Payment.Cols[COL_DOSFROM].Visible = false;// 50;
                c1Payment.Cols[COL_DOSTO].Visible = false;// 0;
                c1Payment.Cols[COL_PATIENTID].Visible = false;// 0;
                c1Payment.Cols[COL_SOURCE].Visible = true;// 100;
                c1Payment.Cols[COL_ORIGINALPAYMENT].Visible = true;// 100;
                c1Payment.Cols[COL_TORESERVES].Visible = true;// 100;
                c1Payment.Cols[COL_TYPE].Visible = true;// 100;
                c1Payment.Cols[COL_NOTE].Visible = true;// 100;
                c1Payment.Cols[COL_AVAILABLE].Visible = true;// 100;
                c1Payment.Cols[COL_USENOW].Visible = true;// 100;
                c1Payment.Cols[COL_PAYMODE].Visible = false;// 100;
                c1Payment.Cols[COL_REFEOBPAYID].Visible = false;// 0;
                c1Payment.Cols[COL_REFEOBPAYDTLID].Visible = false;// 0;
                c1Payment.Cols[COL_ACCOUNTID].Visible = false;// 0;
                c1Payment.Cols[COL_ACCOUNTTYPE].Visible = false;// 0;
                c1Payment.Cols[COL_MSTACCOUNTID].Visible = false;// 0;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].Visible = false;// 0;
                c1Payment.Cols[COL_USERESERVE].Visible = false;
                c1Payment.Cols[COL_RES_EOBPAYID].Visible = false;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].Visible = false;

                #endregion

                #region " Width "
                bool _designWidth = false;
                //try
                //{
                //    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
                //    if (c1Payment.Name == c1SinglePayment.Name)
                //    {
                //        _designWidth = oSetting.LoadGridColumnWidth(c1Payment, gloSettings.ModuleOfGridColumn.PaymentSinglePaymentGrid, _userId);
                //    }
                //    else if (c1Payment.Name == c1MultiplePayment.Name)
                //    {
                //        _designWidth = oSetting.LoadGridColumnWidth(c1Payment, gloSettings.ModuleOfGridColumn.PaymentMultiplePaymentGrid, _userId);
                //    }
                //    oSetting.Dispose();
                //}
                //catch (Exception ex)
                //{

                //}

                if (_designWidth == false)
                {

                    c1Payment.Cols[COL_EOBPAYMENTID].Width = 0;
                    c1Payment.Cols[COL_EOBID].Width = 0;
                    c1Payment.Cols[COL_EOBDTLID].Width = 0;
                    c1Payment.Cols[COL_EOBPAYMENTDTLID].Width = 0;
                    c1Payment.Cols[COL_BLTRANSACTIONID].Width = 0;
                    c1Payment.Cols[COL_BLTRANDTLID].Width = 0;
                    c1Payment.Cols[COL_BLTRANLINEID].Width = 0;
                    c1Payment.Cols[COL_DOSFROM].Width = 50;
                    c1Payment.Cols[COL_DOSTO].Width = 0;
                    c1Payment.Cols[COL_PATIENTID].Width = 0;
                    c1Payment.Cols[COL_SOURCE].Width = 100;
                    c1Payment.Cols[COL_ORIGINALPAYMENT].Width = 250;
                    c1Payment.Cols[COL_TORESERVES].Width = 80;
                    c1Payment.Cols[COL_TYPE].Width = 100;
                    c1Payment.Cols[COL_NOTE].Width = 250;
                    c1Payment.Cols[COL_AVAILABLE].Width = 75;
                    c1Payment.Cols[COL_USENOW].Width = 75;
                    c1Payment.Cols[COL_PAYMODE].Width = 100;
                    c1Payment.Cols[COL_REFEOBPAYID].Width = 0;
                    c1Payment.Cols[COL_REFEOBPAYDTLID].Width = 0;
                    c1Payment.Cols[COL_ACCOUNTID].Width = 0;
                    c1Payment.Cols[COL_ACCOUNTTYPE].Width = 0;
                    c1Payment.Cols[COL_MSTACCOUNTID].Width = 0;
                    c1Payment.Cols[COL_MSTACCOUNTTYPE].Width = 0;
                    c1Payment.Cols[COL_USERESERVE].Width = 0;
                    c1Payment.Cols[COL_RES_EOBPAYID].Width = 0;
                    c1Payment.Cols[COL_RES_EOBPAYDTLID].Width = 0;
                }

                #endregion

                #region " Data Type "

                c1Payment.Cols[COL_EOBPAYMENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_EOBPAYMENTDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANSACTIONID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BLTRANLINEID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_DOSFROM].DataType = typeof(System.String);
                c1Payment.Cols[COL_DOSTO].DataType = typeof(System.String);
                c1Payment.Cols[COL_PATIENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_SOURCE].DataType = typeof(System.String);
                c1Payment.Cols[COL_ORIGINALPAYMENT].DataType = typeof(System.String);
                c1Payment.Cols[COL_TORESERVES].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_TYPE].DataType = typeof(System.String);
                c1Payment.Cols[COL_NOTE].DataType = typeof(System.String);
                c1Payment.Cols[COL_AVAILABLE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_USENOW].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_USERESERVE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_PAYMODE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_REFEOBPAYID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_REFEOBPAYDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ACCOUNTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_ACCOUNTTYPE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_MSTACCOUNTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_MSTACCOUNTTYPE].DataType = typeof(System.Int32);
                c1Payment.Cols[COL_RES_EOBPAYID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_RES_EOBPAYDTLID].DataType = typeof(System.Int64);

                #endregion

                #region " Alignment "

                c1Payment.Cols[COL_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_EOBID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_EOBDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_BLTRANSACTIONID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BLTRANDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BLTRANLINEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_DOSFROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_DOSTO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ORIGINALPAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_TORESERVES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_NOTE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_AVAILABLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_USENOW].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_USERESERVE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAYMODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_REFEOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_REFEOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_MSTACCOUNTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_RES_EOBPAYID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1Payment.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1Payment.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                    }

                }
                catch
                {
                    csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }


                C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;// = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableCurrencyStyle"))
                    {
                        csEditableCurrencyStyle = c1Payment.Styles["cs_EditableCurrencyStyle"];
                    }
                    else
                    {
                        csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                        csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csEditableCurrencyStyle.Format = "c";
                        csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableCurrencyStyle.BackColor = Color.White;

                }


                C1.Win.C1FlexGrid.CellStyle csEditableStringStyle;// = c1Payment.Styles.Add("cs_EditableStringStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableStringStyle"))
                    {
                        csEditableStringStyle = c1Payment.Styles["cs_EditableStringStyle"];
                    }
                    else
                    {
                        csEditableStringStyle = c1Payment.Styles.Add("cs_EditableStringStyle");
                        csEditableStringStyle.DataType = typeof(System.String);
                        csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableStringStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableStringStyle = c1Payment.Styles.Add("cs_EditableStringStyle");
                    csEditableStringStyle.DataType = typeof(System.String);
                    csEditableStringStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableStringStyle.BackColor = Color.White;

                }


                C1.Win.C1FlexGrid.CellStyle csEditableDateStyle;// = c1Payment.Styles.Add("cs_EditableDateStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableDateStyle"))
                    {
                        csEditableDateStyle = c1Payment.Styles["cs_EditableDateStyle"];
                    }
                    else
                    {
                        csEditableDateStyle = c1Payment.Styles.Add("cs_EditableDateStyle");
                        csEditableDateStyle.DataType = typeof(System.DateTime);
                        csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableDateStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableDateStyle = c1Payment.Styles.Add("cs_EditableDateStyle");
                    csEditableDateStyle.DataType = typeof(System.DateTime);
                    csEditableDateStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableDateStyle.BackColor = Color.White;

                }



                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle;// = c1Payment.Styles.Add("cs_ClaimRowStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_ClaimRowStyle"))
                    {
                        csClaimRowStyle = c1Payment.Styles["cs_ClaimRowStyle"];
                    }
                    else
                    {
                        csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                        csClaimRowStyle.DataType = typeof(System.String);
                        csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }

                }
                catch
                {
                    csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);

                }



                C1.Win.C1FlexGrid.CellStyle csPatientRowStyle;// = c1Payment.Styles.Add("cs_PatientRowStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_PatientRowStyle"))
                    {
                        csPatientRowStyle = c1Payment.Styles["cs_PatientRowStyle"];
                    }
                    else
                    {
                        csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                        csPatientRowStyle.DataType = typeof(System.String);
                        csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);
                    }

                }
                catch
                {
                    csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                    csPatientRowStyle.DataType = typeof(System.String);
                    csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);
                }


                c1Payment.Cols[COL_TORESERVES].Style = csCurrencyStyle;
                c1Payment.Cols[COL_AVAILABLE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_USENOW].Style = csCurrencyStyle;
                c1Payment.Cols[COL_USERESERVE].Style = csCurrencyStyle;

                #endregion

                c1Payment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1Payment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1Payment.AllowEditing = true;

                c1Payment.Cols[COL_EOBPAYMENTID].AllowEditing = false;
                c1Payment.Cols[COL_EOBID].AllowEditing = false;//0;
                c1Payment.Cols[COL_EOBDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_EOBPAYMENTDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANSACTIONID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_BLTRANLINEID].AllowEditing = false;//0;
                c1Payment.Cols[COL_DOSFROM].AllowEditing = false;//50;
                c1Payment.Cols[COL_DOSTO].AllowEditing = false;//0;
                c1Payment.Cols[COL_PATIENTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_SOURCE].AllowEditing = false;//100;
                c1Payment.Cols[COL_ORIGINALPAYMENT].AllowEditing = false;//100;
                c1Payment.Cols[COL_TORESERVES].AllowEditing = false;//100;
                c1Payment.Cols[COL_TYPE].AllowEditing = false;//100;
                c1Payment.Cols[COL_NOTE].AllowEditing = false;//100;
                c1Payment.Cols[COL_AVAILABLE].AllowEditing = false;//100;
                c1Payment.Cols[COL_USENOW].AllowEditing = true;//100;
                c1Payment.Cols[COL_USERESERVE].AllowEditing = false;//100;
                c1Payment.Cols[COL_PAYMODE].AllowEditing = false;//100;
                c1Payment.Cols[COL_REFEOBPAYID].AllowEditing = false;//0;
                c1Payment.Cols[COL_REFEOBPAYDTLID].AllowEditing = false;//0;
                c1Payment.Cols[COL_ACCOUNTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_ACCOUNTTYPE].AllowEditing = false;//0;
                c1Payment.Cols[COL_MSTACCOUNTID].AllowEditing = false;//0;
                c1Payment.Cols[COL_MSTACCOUNTTYPE].AllowEditing = false;//0;
                c1Payment.Cols[COL_RES_EOBPAYID].AllowEditing = false;//0;
                c1Payment.Cols[COL_RES_EOBPAYDTLID].AllowEditing = false;//0;

                #endregion

                c1Payment.VisualStyle = VisualStyle.Office2007Blue;
                c1Payment.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            { _IsFormLoading = false; c1Payment.Redraw = true; }
        }

        #endregion " Design Grid "

        #region " Grid Events "

        private void c1Reserve_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int _rowIndex = 0;
                _rowIndex = c1Reserve.HitTest(e.X, e.Y).Row;
                if (_rowIndex > 0)
                { OpenReserveForModify(_rowIndex); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1Reserve_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (c1Reserve.ColSel == COL_SOURCE)
                {
                    c1Reserve.Select(c1Reserve.RowSel, COL_USENOW);
                }
            }
        }

        private void c1Reserve_CellChanged(object sender, RowColEventArgs e)
        {
            try
            {
                if (_IsFormLoading == false && _isFormClosing == false)
                {
                    _isValidResAmount = true;
                    if (e.Row > 0 && e.Col == COL_USENOW)
                    {
                        if (c1Reserve.GetData(e.Row, e.Col) != null && Convert.ToString(c1Reserve.GetData(e.Row, e.Col)).Trim() != ""
                         && c1Reserve.GetData(e.Row, COL_AVAILABLE) != null && Convert.ToString(c1Reserve.GetData(e.Row, COL_AVAILABLE)).Trim() != "")
                        {
                            if (Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_USENOW)) < 0)
                            {
                                try
                                {
                                    this.c1Reserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
                                    MessageBox.Show("Use amount cannot be negative. Please select a valid amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    _isValidResAmount = false;
                                    c1Reserve.SetData(e.Row, e.Col, null);
                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    ex = null;
                                }
                                finally
                                { this.c1Reserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged); }
                            }
                            else if (Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_USENOW)) > Convert.ToDecimal(c1Reserve.GetData(e.Row, COL_AVAILABLE)))
                            {
                                try
                                {
                                    this.c1Reserve.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
                                    MessageBox.Show("Use amount cannot be more than the available amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    _isValidResAmount = false;
                                    c1Reserve.SetData(e.Row, e.Col, null);
                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    ex = null;
                                }
                                finally
                                { this.c1Reserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged); }

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
        }

        #endregion

        #region " Private & Public Methods "

        //addded by mahesh s on 10-may-2011
        private void FillReserves_PAF(Int64 nPAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtReserves = new DataTable();

            try
            {

                DesignPaymentGrid(c1Reserve);

                _IsFormLoading = true;

                oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//NUMERIC(18,0)
                oParameters.Add("@nEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0)

                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_PaymentTransaction_UseReserve", oParameters, out _dtReserves);
                //oDB.Retrive("PA_SELECT_PaymentTransaction_UseReserve_PAF", oParameters, out _dtReserves);
                oDB.Disconnect();

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

                        string _originalPayment = "";
                        _originalPayment = ((EOBPaymentMode)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])).ToString() + "# " + Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]) + " " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dtReserves.Rows[i]["nCheckDate"])).ToString("MM/dd/yyyy") + " $ " + Convert.ToDecimal(_dtReserves.Rows[i]["nCheckAmount"]);
                        c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                        c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]));
                        c1Reserve.SetData(_rowIndex, COL_TYPE, ((EOBPaymentSubType)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                        c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]));//Note
                        c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(_dtReserves.Rows[i]["UsedReserve"]));//Used amount
                        c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount
                        c1Reserve.SetData(_rowIndex, COL_USENOW, 0);//Current amount to use from avaiable amount

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
                        //Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"])
                        //
                        //

                        #region " Set Styles "

                        c1Reserve.SetCellStyle(_rowIndex, COL_USENOW, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

                        #endregion " Set Styles "


                        #endregion
                    }
                }


            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtReserves != null) { _dtReserves.Dispose(); }
                _IsFormLoading = false;
            }
        }

        private void FillReserves()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtReserves = new DataTable();

            try
            {

                DesignPaymentGrid(c1Reserve);

                _IsFormLoading = true;

                //oParameters.Add("@nPatientID", _patientId, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nPAccountID", PAccountID, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//NUMERIC(18,0)
                oParameters.Add("@nEOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0),
                oParameters.Add("@nEOBPaymentDetailID", 0, ParameterDirection.Input, SqlDbType.BigInt);// NUMERIC(18,0)

                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_PaymentTransaction_UseReserve", oParameters, out _dtReserves);
                oDB.Disconnect();

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

                        string _originalPayment = "";
                        _originalPayment = ((EOBPaymentMode)Convert.ToInt32(_dtReserves.Rows[i]["nPayMode"])).ToString() + "# " + Convert.ToString(_dtReserves.Rows[i]["CheckNumber"]) + " " + gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_dtReserves.Rows[i]["nCheckDate"])).ToString("MM/dd/yyyy") + " $ " + Convert.ToDecimal(_dtReserves.Rows[i]["nCheckAmount"]);
                        c1Reserve.SetData(_rowIndex, COL_ORIGINALPAYMENT, _originalPayment);//Check Number,Date,Amount

                        c1Reserve.SetData(_rowIndex, COL_TORESERVES, Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"]));
                        c1Reserve.SetData(_rowIndex, COL_TYPE, ((EOBPaymentSubType)Convert.ToInt32(_dtReserves.Rows[i]["nPaymentNoteSubType"])).ToString());//Copay,Advance,Other
                        c1Reserve.SetData(_rowIndex, COL_NOTE, Convert.ToString(_dtReserves.Rows[i]["sNoteDescription"]));//Note
                        c1Reserve.SetData(_rowIndex, COL_USERESERVE, Convert.ToDecimal(_dtReserves.Rows[i]["UsedReserve"]));//Used amount
                        c1Reserve.SetData(_rowIndex, COL_AVAILABLE, Convert.ToDecimal(_dtReserves.Rows[i]["AvailableReserve"]));//Available amount
                        c1Reserve.SetData(_rowIndex, COL_USENOW, 0);//Current amount to use from avaiable amount

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
                        //Convert.ToDecimal(_dtReserves.Rows[i]["nAmount"])
                        //
                        //

                        #region " Set Styles "

                        c1Reserve.SetCellStyle(_rowIndex, COL_USENOW, c1Reserve.Styles["cs_EditableCurrencyStyle"]);

                        #endregion " Set Styles "


                        #endregion
                    }
                }


            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtReserves != null) { _dtReserves.Dispose(); }
                _IsFormLoading = false;
            }
        }

        #endregion " Private & Public Methods "



    }
}