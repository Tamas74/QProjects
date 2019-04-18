using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloBilling.Payment;
using C1.Win.C1FlexGrid;
using gloBilling;

namespace gloAccountsV2
{
    public partial class frmViewClaimRemittanceV2 : Form
    {
        #region "Variable declaration"

        private string _sqlDatabaseConnectionString = "";
        private Int64 _nPatientID = 0;
        private Int64 _nClinicID = 1;
        private Int64 _nEEOBPmtID = 0;
        private Int64 _nEEOBID = 0;
        private Boolean _isRtnViewPmntForm = false;

        private Int64 _nVoidRefEOBPaymentID = 0;
        private Int64 _nVoidEOBID = 0;
        private bool _nIsVoidEOBPayment = false;
        private Label label;
        private bool _isInvokedFromInsurancePayment = false;
        SSRSApplication.frmSSRSViewer frmSSRS = null;

        public Int64 VoidRefEOBPaymentID
        {
            get { return _nVoidRefEOBPaymentID; }
            set { _nVoidRefEOBPaymentID = value; }
        }
        public Int64 VoidEOBID
        {
            get { return _nVoidEOBID; }
            set { _nVoidEOBID = value; }
        }
        public bool IsVoidEOBPayment
        {
            get { return _nIsVoidEOBPayment; }
            set { _nIsVoidEOBPayment = value; }
        }

        public string CallingContainer { get; set; }

        private const int COL_NEOBID = 0;
        private const int COL_NEOBDETAILID = 1;
        private const int COL_NEOBPAYMENTID = 2;

        private const int COL_NBILLINGTRANSACTIONID = 3;
        private const int COL_NBILLINGTRANSACTIONDETAILID = 4;
        private const int COL_NDOSFROM = 5;
        private const int COL_SCPTCODE = 6;
        private const int COL_SCPTDESCRIPTION = 7;

        private const int COL_DTOTALCHARGES = 8;
        private const int COL_DALLOWED = 9;
        private const int COL_DPAYMENT = 10;

        private const int COL_DWRITEOFF = 11;
        private const int COL_DCOPAY = 12;
        private const int COL_DDEDUCTIBLE = 13;
        private const int COL_DCOINSURANCE = 14;
        private const int COL_DWITHHOLD = 15;
        private const int COL_DNOTCOVERED = 16;

        #endregion

        public Int64 TransactionID { get; set; }
        public Int64 ContactID { get; set; }

        public frmViewClaimRemittanceV2(string databaseconnectionstring, Int64 patientid, Int64 clinicid, Int64 nEEOBPmtID, Int64 nEEOBID, bool IsInsurancePaymentCall = false)
        {
            InitializeComponent();
            _nEEOBPmtID = nEEOBPmtID;
            _nEEOBID = nEEOBID;
            _sqlDatabaseConnectionString = databaseconnectionstring;
            _nClinicID = clinicid;
            _nPatientID = patientid;
            _isInvokedFromInsurancePayment = IsInsurancePaymentCall;
        }

        private void frmViewClaimRemittanceV2_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(c1FlexRemittance, false);
            gloC1FlexStyle.Style(c1FlexDeltaRemittance, false);

            switch (CallingContainer)
            {
                case "frmViewInsurancePaymentV2":
                    tsbViewInsPmnt.Visible = false;
                    tsbViewHistory.Visible = true;
                    break;
                case "frmClaimChargeHistory":
                    tsbViewInsPmnt.Visible = true;
                    tsbViewHistory.Visible = false;
                    break;
                default:
                    tsbViewInsPmnt.Visible = true;
                    tsbViewHistory.Visible = false;
                    break;
            }
            this.c1FlexRemittance.EnterCell -= new System.EventHandler(this.c1FlexRemittance_EnterCell);
            this.c1FlexDeltaRemittance.EnterCell -= new System.EventHandler(this.c1FlexDeltaRemittance_EnterCell);
            LoadFormData();
            this.c1FlexRemittance.EnterCell += new System.EventHandler(this.c1FlexRemittance_EnterCell);
            this.c1FlexDeltaRemittance.EnterCell += new System.EventHandler(this.c1FlexDeltaRemittance_EnterCell);
        }

        #region " Methods and Procedures "

        private void LoadFormData()
        {
            gloPatientFinancialViewV2 objClsPatientFinancialViewV2 = new gloPatientFinancialViewV2(_nPatientID);
            DataRow _drLogDetails = null;
            try
            {
                _drLogDetails = objClsPatientFinancialViewV2.GetInsurancePaymentLogDetails(_nEEOBPmtID, TransactionID, ContactID);

                if (_drLogDetails != null)
                {
                    if (_drLogDetails["PaymentDate"] != null)
                    { lblCheckDate.Text = Convert.ToDateTime(_drLogDetails["PaymentDate"]).ToString("MM/dd/yyyy"); }

                    if (_drLogDetails["Tray"] != null)
                    {
                        lblPaymentTray.Text = Convert.ToString(_drLogDetails["Tray"]);
                        if (_drLogDetails["Tray"].ToString().Length > 40)
                        {
                            C1SuperTooltipDx.SetToolTip(lblPaymentTray, _drLogDetails["Tray"].ToString());
                        }
                        else
                        {
                            C1SuperTooltipDx.SetToolTip(lblPaymentTray, "");
                        }
                    }

                    if (_drLogDetails["Company"] != null)
                    { lblInsCompany.Text = Convert.ToString(_drLogDetails["Company"]); }

                    if (_drLogDetails["CloseDate"] != null)
                    { lblCloseDate.Text = Convert.ToDateTime(_drLogDetails["CloseDate"]).ToString("MM/dd/yyyy"); }

                    if (_drLogDetails["CheckNumber"] != null)
                    { lblChkNo.Text = Convert.ToString(_drLogDetails["CheckNumber"]).Replace("&", "&&"); }

                    if (_drLogDetails["sClaimRemittanceRefNo"] != null)
                    { lblRefNo.Text = Convert.ToString(_drLogDetails["sClaimRemittanceRefNo"]); }

                }
                #region " Alert message for voided check "

                DataTable _dtPaymentVoid = null;

                _dtPaymentVoid = objClsPatientFinancialViewV2.GetVoidedInsurancePayment(_nEEOBPmtID);

                if (_dtPaymentVoid != null && _dtPaymentVoid.Rows.Count > 0)
                {
                    lblAlertMessage.Visible = true;
                    lblAlertMessage.Text = "Voided [" + Convert.ToString(_dtPaymentVoid.Rows[0]["sUserName"].ToString()) + "] on " + Convert.ToString(_dtPaymentVoid.Rows[0]["nVoidCloseDate"].ToString());
                }

                #endregion

                LoadPaymentLogDetails();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (objClsPatientFinancialViewV2 != null) { objClsPatientFinancialViewV2.Dispose(); }
            }
        }




        private void LoadPaymentLogDetails()
        {
            gloPatientFinancialViewV2 objClsPatientFinancialViewV2 = new gloPatientFinancialViewV2(_nPatientID);
            try
            {
                DataTable _dtEOBPayments = new DataTable();
                DataTable _dtEOBPaymentsReason = new DataTable();
                DataTable _dtEOBPreviousPaymentsReason = new DataTable();
                if (_isRtnViewPmntForm == true)
                {

                    c1FlexRemittance.Rows.Count = 1;
                    c1FlexDeltaRemittance.Rows.Count = 1;

                }

                {

                    _dtEOBPayments = objClsPatientFinancialViewV2.GetEOBPaymentSummary(_nEEOBPmtID, _nEEOBID);

                    string PaymentNotes = "";
                    string StatementsNotes = "";

                    Int32 counter = 0;
                    Int32 counter1 = 0;
                    if (_dtEOBPayments.Rows.Count > 0)
                    {
                        lblClaim.Text = Convert.ToString(_dtEOBPayments.Rows[0]["ClaimNumber"]);
                        lblClaim.Tag = Convert.ToString(_dtEOBPayments.Rows[0]["nTransactionID"]);

                        lblPatient.Text = Convert.ToString(_dtEOBPayments.Rows[0]["PatientCode"]) + " - " + Convert.ToString(_dtEOBPayments.Rows[0]["PatientName"]);
                        lblPatient.Tag = Convert.ToInt64(_dtEOBPayments.Rows[0]["nPatientID"]);
                        //lblPlan.Text = Convert.ToString(_dtEOBPayments.Rows[0]["ResponsibilityNo"]) + " - " + Convert.ToString(_dtEOBPayments.Rows[0]["ContactName"]);
                        lblPlan.Text = Convert.ToString(_dtEOBPayments.Rows[0]["ContactName"]);

                        if (_dtEOBPayments.Select("IsCorrection = 'True'").Length > 0)
                        {
                            pnlCorrection.Visible = true;
                            pnlTotalCaptionPanel.Visible = true;
                        }
                        else
                        {
                            pnlCorrection.Visible = false;
                            pnlTotalCaptionPanel.Visible = false;

                        }

                        DataView dv = _dtEOBPayments.DefaultView;
                        DataTable dtUniqueData = dv.ToTable(true, "nBillingTransactionDetailID");
                        DataTable dtFilterData;
                        dtFilterData = _dtEOBPayments.Clone();
                        for (int cntr = 0; cntr <= dtUniqueData.Rows.Count - 1; cntr++)
                        {
                            PaymentNotes = "";
                            StatementsNotes = "";
                            //ReasonNotes = "";
                            c1FlexRemittance.Rows.Add();
                            counter = counter + 1;
                            c1FlexRemittance.SetData(counter, COL_NEOBID, _dtEOBPayments.Rows[cntr]["nEOBID"]);
                            c1FlexRemittance.SetData(counter, COL_NEOBDETAILID, _dtEOBPayments.Rows[cntr]["nEOBDtlID"]);
                            c1FlexRemittance.SetData(counter, COL_NEOBPAYMENTID, _dtEOBPayments.Rows[cntr]["nEOBPaymentID"]);
                            c1FlexRemittance.SetData(counter, COL_NBILLINGTRANSACTIONID, _dtEOBPayments.Rows[cntr]["nBillingTransactionID"]);
                            c1FlexRemittance.SetData(counter, COL_NBILLINGTRANSACTIONDETAILID, _dtEOBPayments.Rows[cntr]["nBillingTransactionDetailID"]);
                            c1FlexRemittance.SetData(counter, COL_NDOSFROM, _dtEOBPayments.Rows[cntr]["nDOSFrom"]);
                            c1FlexRemittance.SetData(counter, COL_SCPTCODE, _dtEOBPayments.Rows[cntr]["scptcode"]);
                            c1FlexRemittance.SetData(counter, COL_SCPTDESCRIPTION, _dtEOBPayments.Rows[cntr]["scptDescription"]);
                            c1FlexRemittance.SetData(counter, COL_DTOTALCHARGES, _dtEOBPayments.Rows[cntr]["dTotalCharges"]);
                            if (_nIsVoidEOBPayment == true)
                            {
                                c1FlexRemittance.SetData(counter, COL_DPAYMENT, (-1) * Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["dPayment"] == DBNull.Value ? 0 : _dtEOBPayments.Rows[cntr]["dPayment"]));
                                c1FlexRemittance.SetData(counter, COL_DALLOWED, (-1) * Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["dAllowed"] == DBNull.Value ? 0 : _dtEOBPayments.Rows[cntr]["dAllowed"]));
                                c1FlexRemittance.SetData(counter, COL_DWRITEOFF, (-1) * Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["dWriteOff"] == DBNull.Value ? 0 : _dtEOBPayments.Rows[cntr]["dWriteOff"]));
                                c1FlexRemittance.SetData(counter, COL_DCOPAY, (-1) * Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["dCopay"] == DBNull.Value ? 0 : _dtEOBPayments.Rows[cntr]["dCopay"]));
                                c1FlexRemittance.SetData(counter, COL_DDEDUCTIBLE, (-1) * Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["dDeductible"] == DBNull.Value ? 0 : _dtEOBPayments.Rows[cntr]["dDeductible"]));
                                c1FlexRemittance.SetData(counter, COL_DWITHHOLD, (-1) * Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["dWithhold"] == DBNull.Value ? 0 : _dtEOBPayments.Rows[cntr]["dWithhold"]));
                                c1FlexRemittance.SetData(counter, COL_DCOINSURANCE, (-1) * Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["dCoInsurance"] == DBNull.Value ? 0 : _dtEOBPayments.Rows[cntr]["dCoInsurance"]));
                                c1FlexRemittance.SetData(counter, COL_DNOTCOVERED, (-1) * Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["Other"] == DBNull.Value ? 0 : _dtEOBPayments.Rows[cntr]["Other"]));
                                pnlCorrection.Visible = false;
                                pnlTotalCaptionPanel.Visible = false;
                            }
                            else
                            {
                                c1FlexRemittance.SetData(counter, COL_DPAYMENT, _dtEOBPayments.Rows[cntr]["dPayment"]);
                                c1FlexRemittance.SetData(counter, COL_DALLOWED, _dtEOBPayments.Rows[cntr]["dAllowed"]);
                                c1FlexRemittance.SetData(counter, COL_DWRITEOFF, _dtEOBPayments.Rows[cntr]["dWriteOff"]);
                                c1FlexRemittance.SetData(counter, COL_DCOPAY, _dtEOBPayments.Rows[cntr]["dCopay"]);
                                c1FlexRemittance.SetData(counter, COL_DDEDUCTIBLE, _dtEOBPayments.Rows[cntr]["dDeductible"]);
                                c1FlexRemittance.SetData(counter, COL_DWITHHOLD, _dtEOBPayments.Rows[cntr]["dWithhold"]);
                                c1FlexRemittance.SetData(counter, COL_DCOINSURANCE, _dtEOBPayments.Rows[cntr]["dCoInsurance"]);
                                c1FlexRemittance.SetData(counter, COL_DNOTCOVERED, _dtEOBPayments.Rows[cntr]["Other"]);

                            }

                            //Adding Previous EOB Reasons and Hiding the Row for Total grid
                            DataTable _dtEOBPreviousPaymentRemark;
                            _dtEOBPreviousPaymentsReason = objClsPatientFinancialViewV2.GetEOBPaymentReason(Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nPreviousEOBID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nEOBPaymentID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionDetailID"]));
                            if (_dtEOBPreviousPaymentsReason.Rows.Count > 0)
                            {
                                for (int i = 0; i <= _dtEOBPreviousPaymentsReason.Rows.Count - 1; i++)
                                {
                                    c1FlexRemittance.Rows.Add();
                                    counter = counter + 1;
                                    c1FlexRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                    CellRange cs = c1FlexRemittance.GetCellRange(counter, 5, counter, 16);
                                    cs.Data = _dtEOBPreviousPaymentsReason.Rows[i]["CODE"].ToString() + " - ($" + _dtEOBPreviousPaymentsReason.Rows[i]["Amount"].ToString() + ") - " + _dtEOBPreviousPaymentsReason.Rows[i]["Reason"].ToString();
                                    c1FlexRemittance.MergedRanges.Add(cs, false);
                                    c1FlexRemittance.Rows[c1FlexRemittance.Rows.Count - 1].Visible = false;
                                    _dtEOBPreviousPaymentRemark = objClsPatientFinancialViewV2.GetEOBPaymentRemark(Convert.ToInt64(_dtEOBPreviousPaymentsReason.Rows[i]["nEOBID"]), Convert.ToInt64(_dtEOBPreviousPaymentsReason.Rows[i]["nEOBPaymentID"]), Convert.ToInt64(_dtEOBPreviousPaymentsReason.Rows[i]["nBillingTransactionID"]), Convert.ToInt64(_dtEOBPreviousPaymentsReason.Rows[i]["nBillingTransactionDetailID"]), _dtEOBPreviousPaymentsReason.Rows[i]["CODE"].ToString());

                                    if (_dtEOBPreviousPaymentRemark.Rows.Count > 0)
                                    {
                                        for (int ind = 0; ind <= _dtEOBPreviousPaymentRemark.Rows.Count - 1; ind++)
                                        {
                                            c1FlexRemittance.Rows.Add();
                                            counter = counter + 1;
                                            c1FlexRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                            CellRange csRemark = c1FlexRemittance.GetCellRange(counter, 5, counter, 16);
                                            csRemark.Data = _dtEOBPreviousPaymentRemark.Rows[ind]["Code"].ToString() + " - " + _dtEOBPreviousPaymentRemark.Rows[ind]["Remark"].ToString();
                                            c1FlexRemittance.MergedRanges.Add(csRemark, false);
                                            c1FlexRemittance.Rows[c1FlexRemittance.Rows.Count - 1].Visible = false;
                                        }
                                    }

                                }
                            }

                            //********

                            //counter = counter + 1;
                            _dtEOBPaymentsReason = objClsPatientFinancialViewV2.GetEOBPaymentReason(Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nEOBID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nEOBPaymentID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionDetailID"]));
                            DataTable _dtEOBPaymentRemark;
                            if (_dtEOBPaymentsReason.Rows.Count > 0)
                            {
                                for (int i = 0; i <= _dtEOBPaymentsReason.Rows.Count - 1; i++)
                                {
                                    c1FlexRemittance.Rows.Add();
                                    counter = counter + 1;
                                    c1FlexRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                    CellRange cs = c1FlexRemittance.GetCellRange(counter, 5, counter, 16);
                                    cs.Data = _dtEOBPaymentsReason.Rows[i]["CODE"].ToString() + " - $" + _dtEOBPaymentsReason.Rows[i]["Amount"].ToString() + " - " + _dtEOBPaymentsReason.Rows[i]["Reason"].ToString();
                                    c1FlexRemittance.MergedRanges.Add(cs, false);

                                    _dtEOBPaymentRemark = objClsPatientFinancialViewV2.GetEOBPaymentRemark(Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nEOBID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nEOBPaymentID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionDetailID"]), _dtEOBPaymentsReason.Rows[i]["CODE"].ToString());

                                    if (_dtEOBPaymentRemark.Rows.Count > 0)
                                    {
                                        for (int ind = 0; ind <= _dtEOBPaymentRemark.Rows.Count - 1; ind++)
                                        {
                                            c1FlexRemittance.Rows.Add();
                                            counter = counter + 1;
                                            c1FlexRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                            CellRange csRemark = c1FlexRemittance.GetCellRange(counter, 5, counter, 16);
                                            csRemark.Data = _dtEOBPaymentRemark.Rows[ind]["CODE"].ToString() + " - " + _dtEOBPaymentRemark.Rows[ind]["Remark"].ToString();
                                            c1FlexRemittance.MergedRanges.Add(csRemark, false);

                                        }
                                    }
                                }
                            }
                            //else if (_dtEOBPaymentsReason.Rows.Count == 0)
                            {
                                _dtEOBPaymentRemark = objClsPatientFinancialViewV2.GetEOBPaymentRemark(Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nEOBID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nEOBPaymentID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionDetailID"]), "");

                                if (_dtEOBPaymentRemark.Rows.Count > 0)
                                {
                                    for (int ind = 0; ind <= _dtEOBPaymentRemark.Rows.Count - 1; ind++)
                                    {
                                        c1FlexRemittance.Rows.Add();
                                        counter = counter + 1;
                                        c1FlexRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                        CellRange csRemark = c1FlexRemittance.GetCellRange(counter, 5, counter, 16);
                                        csRemark.Data = _dtEOBPaymentRemark.Rows[ind]["CODE"].ToString() + " - " + _dtEOBPaymentRemark.Rows[ind]["Remark"].ToString();
                                        c1FlexRemittance.MergedRanges.Add(csRemark, false);

                                    }
                                }
                            }



                            DataRow[] resultRows = null;
                            resultRows = _dtEOBPayments.Select("nBillingTransactionDetailID=" + dtUniqueData.Rows[cntr]["nBillingTransactionDetailID"]);
                            if (resultRows.Length > 0)
                            {
                                foreach (DataRow dr in resultRows)
                                {
                                    //StatementNotes
                                    if (dr["StatementNote"].ToString() != "")
                                    {
                                        if (StatementsNotes == "")
                                            StatementsNotes = dr["StatementNote"].ToString();
                                        else
                                            StatementsNotes = StatementsNotes + ", " + dr["StatementNote"].ToString();
                                    }

                                    //PaymentNotes
                                    if (dr["PaymentNote"].ToString() != "")
                                    {
                                        if (PaymentNotes == "")
                                            PaymentNotes = dr["PaymentNote"].ToString();
                                        else
                                            PaymentNotes = PaymentNotes + ", " + dr["PaymentNote"].ToString();
                                    }

                                }
                                if (StatementsNotes != "")
                                {
                                    c1FlexRemittance.Rows.Add();
                                    counter = counter + 1;
                                    c1FlexRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                    CellRange cs = c1FlexRemittance.GetCellRange(counter, 5, counter, 16);
                                    cs.Data = StatementsNotes;

                                    c1FlexRemittance.MergedRanges.Add(cs, false);
                                    //string tip = (string)c1FlexRemittance.Cols[c1FlexRemittance.MouseCol].Style.UserData;
                                    //C1SuperTooltipDx.SetToolTip(c1FlexRemittance, "");
                                }
                                if (PaymentNotes != "")
                                {
                                    c1FlexRemittance.Rows.Add();
                                    counter = counter + 1;
                                    c1FlexRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                    CellRange cs = c1FlexRemittance.GetCellRange(counter, 5, counter, 16);
                                    cs.Data = PaymentNotes;
                                    c1FlexRemittance.MergedRanges.Add(cs, false);
                                }

                            }

                            //** Filling Delta Grid

                            PaymentNotes = "";
                            StatementsNotes = "";
                            //ReasonNotes = "";
                            c1FlexDeltaRemittance.Rows.Add();
                            counter1 = counter1 + 1;
                            c1FlexDeltaRemittance.SetData(counter1, COL_NEOBID, _dtEOBPayments.Rows[cntr]["nEOBID"]);
                            c1FlexDeltaRemittance.SetData(counter1, COL_NEOBDETAILID, _dtEOBPayments.Rows[cntr]["nEOBDtlID"]);
                            c1FlexDeltaRemittance.SetData(counter1, COL_NEOBPAYMENTID, _dtEOBPayments.Rows[cntr]["nEOBPaymentID"]);
                            c1FlexDeltaRemittance.SetData(counter1, COL_NBILLINGTRANSACTIONID, _dtEOBPayments.Rows[cntr]["nBillingTransactionID"]);
                            c1FlexDeltaRemittance.SetData(counter1, COL_NBILLINGTRANSACTIONDETAILID, _dtEOBPayments.Rows[cntr]["nBillingTransactionDetailID"]);
                            c1FlexDeltaRemittance.SetData(counter1, COL_NDOSFROM, _dtEOBPayments.Rows[cntr]["nDOSFrom"]);
                            c1FlexDeltaRemittance.SetData(counter1, COL_SCPTCODE, _dtEOBPayments.Rows[cntr]["scptcode"]);
                            c1FlexDeltaRemittance.SetData(counter1, COL_SCPTDESCRIPTION, _dtEOBPayments.Rows[cntr]["scptDescription"]);
                            c1FlexDeltaRemittance.SetData(counter1, COL_DTOTALCHARGES, _dtEOBPayments.Rows[cntr]["dTotalCharges"]);

                            if (_dtEOBPayments.Select("IsCorrection = 'True'").Length > 0)
                            {
                                if (_dtEOBPayments.Rows[cntr]["dLastpayment"] != DBNull.Value && Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["dLastpayment"]) != 0)
                                {
                                    c1FlexDeltaRemittance.SetData(counter1, COL_DPAYMENT, _dtEOBPayments.Rows[cntr]["dLastpayment"]);
                                }
                                if (_dtEOBPayments.Rows[cntr]["deltaAllowed"] != DBNull.Value && Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["deltaAllowed"]) != 0)
                                {
                                    c1FlexDeltaRemittance.SetData(counter1, COL_DALLOWED, _dtEOBPayments.Rows[cntr]["deltaAllowed"]);
                                }
                                if (_dtEOBPayments.Rows[cntr]["dLastWriteOff"] != DBNull.Value && Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["dLastWriteOff"]) != 0)
                                {
                                    c1FlexDeltaRemittance.SetData(counter1, COL_DWRITEOFF, _dtEOBPayments.Rows[cntr]["dLastWriteOff"]);
                                }
                                if (_dtEOBPayments.Rows[cntr]["deltaCopay"] != DBNull.Value && Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["deltaCopay"]) != 0)
                                {
                                    c1FlexDeltaRemittance.SetData(counter1, COL_DCOPAY, _dtEOBPayments.Rows[cntr]["deltaCopay"]);

                                }
                                if (_dtEOBPayments.Rows[cntr]["deltaDeduct"] != DBNull.Value && Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["deltaDeduct"]) != 0)
                                {
                                    c1FlexDeltaRemittance.SetData(counter1, COL_DDEDUCTIBLE, _dtEOBPayments.Rows[cntr]["deltaDeduct"]);
                                }
                                if (_dtEOBPayments.Rows[cntr]["dLastWithHold"] != DBNull.Value && Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["dLastWithHold"]) != 0)
                                {
                                    c1FlexDeltaRemittance.SetData(counter1, COL_DWITHHOLD, _dtEOBPayments.Rows[cntr]["dLastWithHold"]);
                                }
                                if (_dtEOBPayments.Rows[cntr]["deltaCoIns"] != DBNull.Value && Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["deltaCoIns"]) != 0)
                                {
                                    c1FlexDeltaRemittance.SetData(counter1, COL_DCOINSURANCE, _dtEOBPayments.Rows[cntr]["deltaCoIns"]);
                                }
                                if (_dtEOBPayments.Rows[cntr]["deltOther"] != DBNull.Value && Convert.ToDecimal(_dtEOBPayments.Rows[cntr]["deltOther"]) != 0)
                                {
                                    c1FlexDeltaRemittance.SetData(counter1, COL_DNOTCOVERED, _dtEOBPayments.Rows[cntr]["deltOther"]);
                                }
                            }

                            //Adding Previous EOB Reasons and Hiding the Row for Total grid
                            //_dtEOBPreviousPaymentsReason = GetEOBPaymentReason(Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nPreviousEOBID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nEOBPaymentID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionDetailID"]));
                            if (_dtEOBPreviousPaymentsReason.Rows.Count > 0)
                            {
                                for (int i = 0; i <= _dtEOBPreviousPaymentsReason.Rows.Count - 1; i++)
                                {
                                    c1FlexDeltaRemittance.Rows.Add();
                                    counter1 = counter1 + 1;
                                    c1FlexDeltaRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                    CellRange cs = c1FlexDeltaRemittance.GetCellRange(counter1, 5, counter1, 16);
                                    cs.Data = _dtEOBPreviousPaymentsReason.Rows[i]["CODE"].ToString() + " - ($" + _dtEOBPreviousPaymentsReason.Rows[i]["Amount"].ToString() + ") - " + _dtEOBPreviousPaymentsReason.Rows[i]["Reason"].ToString();
                                    c1FlexDeltaRemittance.MergedRanges.Add(cs, false);
                                    _dtEOBPreviousPaymentRemark = objClsPatientFinancialViewV2.GetEOBPaymentRemark(Convert.ToInt64(_dtEOBPreviousPaymentsReason.Rows[i]["nEOBID"]), Convert.ToInt64(_dtEOBPreviousPaymentsReason.Rows[i]["nEOBPaymentID"]), Convert.ToInt64(_dtEOBPreviousPaymentsReason.Rows[i]["nBillingTransactionID"]), Convert.ToInt64(_dtEOBPreviousPaymentsReason.Rows[i]["nBillingTransactionDetailID"]), _dtEOBPreviousPaymentsReason.Rows[i]["CODE"].ToString());

                                    if (_dtEOBPreviousPaymentRemark.Rows.Count > 0)
                                    {
                                        for (int ind = 0; ind <= _dtEOBPreviousPaymentRemark.Rows.Count - 1; ind++)
                                        {
                                            c1FlexDeltaRemittance.Rows.Add();
                                            counter1 = counter1 + 1;
                                            c1FlexDeltaRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                            CellRange csRemark = c1FlexDeltaRemittance.GetCellRange(counter1, 5, counter1, 16);
                                            csRemark.Data = _dtEOBPreviousPaymentRemark.Rows[ind]["Code"].ToString() + " - " + _dtEOBPreviousPaymentRemark.Rows[ind]["Remark"].ToString();
                                            c1FlexDeltaRemittance.MergedRanges.Add(csRemark, false);

                                        }
                                    }
                                }
                            }
                            //************

                            //counter1 = counter1 + 1;
                            //_dtEOBPaymentsReason = GetEOBPaymentReason(Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nEOBID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nEOBPaymentID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionID"]), Convert.ToInt64(_dtEOBPayments.Rows[cntr]["nBillingTransactionDetailID"]));
                            if (_dtEOBPaymentsReason.Rows.Count > 0)
                            {
                                for (int i = 0; i <= _dtEOBPaymentsReason.Rows.Count - 1; i++)
                                {
                                    c1FlexDeltaRemittance.Rows.Add();

                                    counter1 = counter1 + 1;
                                    c1FlexDeltaRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                    CellRange cs = c1FlexDeltaRemittance.GetCellRange(counter1, 5, counter1, 16);
                                    cs.Data = _dtEOBPaymentsReason.Rows[i]["CODE"].ToString() + " - $" + _dtEOBPaymentsReason.Rows[i]["Amount"].ToString() + " - " + _dtEOBPaymentsReason.Rows[i]["Reason"].ToString();
                                    c1FlexDeltaRemittance.MergedRanges.Add(cs, false);
                                    if (_dtEOBPayments.Select("IsCorrection = 'True'").Length <= 0)
                                    {
                                        c1FlexDeltaRemittance.Rows[c1FlexDeltaRemittance.Rows.Count - 1].Visible = false;
                                    }
                                    else
                                    {
                                        _dtEOBPaymentRemark = objClsPatientFinancialViewV2.GetEOBPaymentRemark(Convert.ToInt64(_dtEOBPaymentsReason.Rows[i]["nEOBID"]), Convert.ToInt64(_dtEOBPaymentsReason.Rows[i]["nEOBPaymentID"]), Convert.ToInt64(_dtEOBPaymentsReason.Rows[i]["nBillingTransactionID"]), Convert.ToInt64(_dtEOBPaymentsReason.Rows[i]["nBillingTransactionDetailID"]), _dtEOBPaymentsReason.Rows[i]["CODE"].ToString());
                                        if (_dtEOBPaymentRemark.Rows.Count > 0)
                                        {
                                            for (int ind = 0; ind <= _dtEOBPaymentRemark.Rows.Count - 1; ind++)
                                            {
                                                c1FlexDeltaRemittance.Rows.Add();
                                                counter1 = counter1 + 1;
                                                c1FlexDeltaRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                                CellRange csRemark = c1FlexDeltaRemittance.GetCellRange(counter1, 5, counter1, 16);
                                                csRemark.Data = _dtEOBPaymentRemark.Rows[ind]["Code"].ToString() + " - " + _dtEOBPaymentRemark.Rows[ind]["Remark"].ToString();
                                                c1FlexDeltaRemittance.MergedRanges.Add(csRemark, false);

                                            }
                                        }
                                    }
                                }
                            }



                            resultRows = null;
                            resultRows = _dtEOBPayments.Select("nBillingTransactionDetailID=" + dtUniqueData.Rows[cntr]["nBillingTransactionDetailID"]);
                            if (resultRows.Length > 0)
                            {
                                foreach (DataRow dr in resultRows)
                                {
                                    //StatementNotes
                                    if (dr["StatementNote"].ToString() != "")
                                    {
                                        if (StatementsNotes == "")
                                            StatementsNotes = dr["StatementNote"].ToString();
                                        else
                                            StatementsNotes = StatementsNotes + ", " + dr["StatementNote"].ToString();
                                    }

                                    //PaymentNotes
                                    if (dr["PaymentNote"].ToString() != "")
                                    {
                                        if (PaymentNotes == "")
                                            PaymentNotes = dr["PaymentNote"].ToString();
                                        else
                                            PaymentNotes = PaymentNotes + ", " + dr["PaymentNote"].ToString();
                                    }

                                }
                                if (StatementsNotes != "")
                                {
                                    c1FlexDeltaRemittance.Rows.Add();

                                    counter1 = counter1 + 1;
                                    c1FlexDeltaRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                    CellRange cs = c1FlexDeltaRemittance.GetCellRange(counter1, 5, counter1, 16);
                                    cs.Data = StatementsNotes;

                                    c1FlexDeltaRemittance.MergedRanges.Add(cs, false);
                                    //string tip = (string)c1FlexDeltaRemittance.Cols[c1FlexDeltaRemittance.MouseCol].Style.UserData;
                                    //C1SuperTooltipDx.SetToolTip(c1FlexDeltaRemittance, "");
                                    if (_dtEOBPayments.Select("IsCorrection = 'True'").Length <= 0)
                                    {
                                        c1FlexDeltaRemittance.Rows[c1FlexDeltaRemittance.Rows.Count - 1].Visible = false;
                                    }
                                }
                                if (PaymentNotes != "")
                                {
                                    c1FlexDeltaRemittance.Rows.Add();

                                    counter1 = counter1 + 1;
                                    c1FlexDeltaRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
                                    CellRange cs = c1FlexDeltaRemittance.GetCellRange(counter1, 5, counter1, 16);
                                    cs.Data = PaymentNotes;
                                    c1FlexDeltaRemittance.MergedRanges.Add(cs, false);
                                    if (_dtEOBPayments.Select("IsCorrection = 'True'").Length <= 0)
                                    {
                                        c1FlexDeltaRemittance.Rows[c1FlexDeltaRemittance.Rows.Count - 1].Visible = false;
                                    }
                                }

                            }


                            //*****
                        }
                    }


                }
                _isRtnViewPmntForm = false;
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _isRtnViewPmntForm = false;
            }
            finally
            {
                if (objClsPatientFinancialViewV2 != null) { objClsPatientFinancialViewV2.Dispose(); }

            }
        }







        #endregion

        private void ts_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbViewInsPmnt_Click(object sender, EventArgs e)
        {
            Int64 nEOBPmntID = 0;
            nEOBPmntID = _nEEOBPmtID;
            frmViewInsurancePaymentV2 ofrmClaimChargeHistory = new frmViewInsurancePaymentV2(nEOBPmntID, _isInvokedFromInsurancePayment);
            ofrmClaimChargeHistory.StartPosition = FormStartPosition.CenterScreen;
            ofrmClaimChargeHistory.ShowDialog(this);
            ofrmClaimChargeHistory.Dispose();
            _isRtnViewPmntForm = true;

            this.c1FlexRemittance.EnterCell -= new System.EventHandler(this.c1FlexRemittance_EnterCell);
            this.c1FlexDeltaRemittance.EnterCell -= new System.EventHandler(this.c1FlexDeltaRemittance_EnterCell);
            LoadFormData();
            this.c1FlexRemittance.EnterCell += new System.EventHandler(this.c1FlexRemittance_EnterCell);
            this.c1FlexDeltaRemittance.EnterCell += new System.EventHandler(this.c1FlexDeltaRemittance_EnterCell);


        }

        private void c1FlexRemittance_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {

                if (c1FlexRemittance.HitTest(e.X, e.Y).Column == COL_SCPTCODE && c1FlexRemittance.HitTest(e.X, e.Y).Column == COL_SCPTCODE)
                {
                    gloC1FlexStyle.ShowToolTip(C1SuperTooltipDx, (C1FlexGrid)sender, e.Location, true);
                }
                else
                {
                    gloC1FlexStyle.ShowToolTip(C1SuperTooltipDx, (C1FlexGrid)sender, e.Location, false);
                    //C1SuperTooltipDx.SetToolTip(c1FlexRemittance, "");
                }
            }

            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
        }

        private void tsbViewHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(lblClaim.Tag) > 0)
                {
                    frmClaimChargeHistoryV2 ofrmClaimChargeHistory = new frmClaimChargeHistoryV2(_sqlDatabaseConnectionString, Convert.ToInt64(lblPatient.Tag), _nClinicID, Convert.ToInt64(lblClaim.Tag));
                    ofrmClaimChargeHistory.StartPosition = FormStartPosition.CenterScreen;
                    ofrmClaimChargeHistory.CallingContainer = this.Name;
                    ofrmClaimChargeHistory.ShowDialog(this);
                    ofrmClaimChargeHistory.Dispose();
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
        }

        private void c1FlexDeltaRemittance_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (c1FlexDeltaRemittance.HitTest(e.X, e.Y).Column == COL_SCPTCODE && c1FlexDeltaRemittance.HitTest(e.X, e.Y).Column == COL_SCPTCODE)
                {
                    gloC1FlexStyle.ShowToolTip(C1SuperTooltipDx, (C1FlexGrid)sender, e.Location, true);
                }
                else
                {
                    gloC1FlexStyle.ShowToolTip(C1SuperTooltipDx, (C1FlexGrid)sender, e.Location, false);
                    //C1SuperTooltipDx.SetToolTip(c1FlexRemittance, "");
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
        }

        private void c1FlexDeltaRemittance_AfterScroll(object sender, RangeEventArgs e)
        {
            int x = c1FlexDeltaRemittance.ScrollPosition.X;
            int y = c1FlexDeltaRemittance.ScrollPosition.Y;
            c1FlexRemittance.ScrollPosition = new Point(x, y);
        }

        private void c1FlexRemittance_AfterScroll(object sender, RangeEventArgs e)
        {
            int x = c1FlexRemittance.ScrollPosition.X;
            int y = c1FlexRemittance.ScrollPosition.Y;
            c1FlexDeltaRemittance.ScrollPosition = new Point(x, y);
        }

        private void c1FlexDeltaRemittance_EnterCell(object sender, EventArgs e)
        {
            try
            {
                c1FlexRemittance.Select(c1FlexDeltaRemittance.RowSel, c1FlexDeltaRemittance.RowSel);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }


        }

        private void c1FlexRemittance_EnterCell(object sender, EventArgs e)
        {
            // c1FlexDeltaRemittance.Select(c1FlexRemittance.RowSel, c1FlexRemittance.ColSel);
        }

        private void lblClaim_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                label = (Label)sender;
                if (lblClaim.Text != null && lblClaim.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblClaim.Text), lblClaim) >= lblClaim.Width - 20)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        toolTip1.SetToolTip(lblClaim, lblClaim.Text);
                    }
                    else
                    {
                        toolTip1.SetToolTip(lblClaim, "");
                    }
                    //toolTip1.SetToolTip(lblInsCompany, SelectedInsuranceCompany);
                }
                else
                {
                    toolTip1.SetToolTip(lblClaim, "");
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private int getWidthofListItems(string _text, Label label)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g != null)
            {
                SizeF s = g.MeasureString(_text, label.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }

            return width;
        }

        private void lblPatient_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                label = (Label)sender;
                if (lblPatient.Text != null && lblPatient.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblPatient.Text), lblClaim) >= lblPatient.Width - 20)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        toolTip1.SetToolTip(lblPatient, lblPatient.Text);
                    }
                    else
                    {
                        toolTip1.SetToolTip(lblPatient, "");
                    }
                    //toolTip1.SetToolTip(lblInsCompany, SelectedInsuranceCompany);
                }
                else
                {
                    toolTip1.SetToolTip(lblPatient, "");
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void ts_btnPrint_Click(object sender, EventArgs e)
        {
            using (frmSSRS = new SSRSApplication.frmSSRSViewer())
            {
                frmSSRS.parameterName = "nCreditID,nEOBID,nEOBType,UserName";
                frmSSRS.ParameterValue = _nEEOBPmtID + "," + _nEEOBID + "," + 4 + "," + gloGlobal.gloPMGlobal.UserName;
                frmSSRS.reportName = "rptClaimRemittance";
                frmSSRS.formIcon = null;
                frmSSRS.reportTitle = "Claim Remittance Report";
                frmSSRS.Conn = _sqlDatabaseConnectionString;
                frmSSRS.IsgloStreamReport = true;
                frmSSRS.ShowDialog(this);
                frmSSRS.Dispose();
                frmSSRS = null;
            }
        }



    }
}

